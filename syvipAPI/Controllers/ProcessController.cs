using Microsoft.AspNetCore.Mvc;
using Entities.All.Models.Admin;
using Entities.All.DTO;
using DataAccess.Repositories;
namespace syvipAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProcessController : ControllerBase
{
    private readonly AdminRepository _adminRepository;

    public ProcessController(AdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
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

    [HttpGet("PReservation")]
    public IActionResult PReservation(SaveReservationDto dto)
    {
        var result = _adminRepository.PReservation(dto);
        return Ok(result);
    }
}
    