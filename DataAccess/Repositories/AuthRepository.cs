using DataAccess.Helper;
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

    public IResult<object> AuthControl(UserInfoForLoginDto dto)
    {
        try
        {
            var founderUser = _context.Users.FirstOrDefault(u => u.UserName == dto.UserName);

            if (founderUser == null)
            {
                return Result<object>.ErrorResult("Kullanıcı adı veya şifre hatalı.");
            }

            var hasher = new PasswordHasher();
            bool isPasswordValid = hasher.VerifyPassword(dto.Password, founderUser.PasswordHash, founderUser.PasswordSalt);

            if (!isPasswordValid)
            {
                return Result<object>.ErrorResult("Kullanıcı adı veya şifre hatalı.");
            }

            string token = _tokenGenerator.GenerateJwtToken(dto.UserName);

            return Result<object>.SuccessResult(new
            {
                Token = token,
                Message = "Giriş başarılı."
            });
        }
        catch
        {
            return Result<object>.ErrorResult("Bir hata oluştu.");
        }
    }
}

