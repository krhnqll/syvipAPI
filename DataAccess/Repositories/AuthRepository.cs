using DataAccess.Helper;
using DataAccess.Service.Abstract;
using DataAccess.Service.Concrete;
using Entities.All.DTO;
using Microsoft.Extensions.Configuration;


namespace DataAccess.Repositories;

public class AuthRepository
{
    private readonly CreateToken _tokenGenerator;
    private readonly AppDbContext _context;

    public AuthRepository(IConfiguration config, AppDbContext context)
    {
        _tokenGenerator = new CreateToken(config);
        _context = context;
    }

    public IResult AuthControl(UserInfoForLoginDto dto)
    {
        try
        {
            var founderUser = _context.Users.FirstOrDefault(u => u.UserName == dto.UserName);

            if (founderUser == null)
            {
                return new ErrorResult("Kullanıcı adı veya şifre hatalı.");
            }

            var hasher = new PasswordHasher();
            bool isPasswordValid = hasher.VerifyPassword(dto.Password, founderUser.PasswordHash, founderUser.PasswordSalt);

            if (!isPasswordValid)
            {
                return new ErrorResult("Kullanıcı adı veya şifre hatalı.");
            }

            string token = _tokenGenerator.GenerateJwtToken(dto.UserName);

            var result = new LoginResponseDTO
            {
                Token = token,
                Message = "Giriş başarılı."
            };

            return new SuccessDataResult<LoginResponseDTO>(result);
        }
        catch
        {
            return new ErrorResult("Bir hata oluştu.");
        }
    }
}

