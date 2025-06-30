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
        // Sabit admin kontrolü (istersen DB'den de kontrol edebilirsin)
        if (dto.UserName == "admin" && dto.Password == "1234")
        {
            string token = _tokenGenerator.GenerateJwtToken(dto.UserName);

            return Result<object>.SuccessResult(new
            {
                Token = token,
                Message = "Giriş başarılı."
            });
        }

        return Result<object>.ErrorResult("Kullanıcı adı veya şifre hatalı.");
    }
}
