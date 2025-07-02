using Microsoft.AspNetCore.Mvc;
using Entities.All.DTO;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace syvipAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ProcessController : ControllerBase
{
    private readonly ProcessRepository _adminRepository;


    public ProcessController(ProcessRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    [HttpGet("Deneme")]
    public IActionResult Deneme()
    {
        var result = _adminRepository.Deneme();
        return Ok(result);
    }

    [HttpGet("GUsers")]
    public IActionResult GUsers()
    {
        var result = _adminRepository.GUsers();
        return Ok(result);
    }

    [HttpGet("GReservations")]
    public IActionResult GReservations()
    {
        var result = _adminRepository.GReservations();
        return Ok(result);
    }

    [HttpGet("GReservationsById")]
    public IActionResult GReservationsById(int id)
    {
        var result = _adminRepository.GReservationsById(id);
        return Ok(result);
    }

    [HttpGet("GAllPhotos")]
    public IActionResult GAllPhotos()
    {
        var result = _adminRepository.GAllPhotos();
        return Ok(result);
    }


    [HttpPost("PSavePhoto")]
    public IActionResult PSavePhoto(SavePhotoDto dto)
    {
        var result = _adminRepository.PSavePhoto(dto);
        return Ok(result);
    }
}
    