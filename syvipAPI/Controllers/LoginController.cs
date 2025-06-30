using Microsoft.AspNetCore.Mvc;
using Entities.All.DTO;
using DataAccess.Repositories;

namespace syvipAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    private readonly AuthRepository _authRepository;
    

    public LoginController(AuthRepository authRepository)
    {
        _authRepository = authRepository;
        
    }

    [HttpGet("AuthControl")]
    public IActionResult AuthControl(UserInfoForLoginDto dto)
    {
        var result = _authRepository.AuthControl(dto);
        return Ok(result); 
    }
}

