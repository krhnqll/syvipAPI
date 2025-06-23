using Entities.All.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AuthRepository 
{
    public IResult<object> AuthControl()
    {
        return Result<object>.SuccessResult("Başarılı.");
    }
}