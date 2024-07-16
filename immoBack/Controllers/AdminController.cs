using immoBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace immoBack.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowSpecificOrigin")]
[Authorize(Policy = "adminPolicy")]
public class AdminController : ControllerBase
{

    private readonly ILogger<AdminController> _logger;
    private readonly IConfiguration _configuration;


    public AdminController(ILogger<AdminController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("admin-chiffre")]
    public IActionResult GetAdminData(DateOnly debut,DateOnly fin)
    {
        Reponse reponse=new Reponse();
        try{
            List<VChiffreAffaire> listes=VChiffreAffaire.GetChiffreAffaireAdmin(debut,fin);
            reponse.data=listes;
        }catch(Exception e){
            reponse.erreur=e.Message;
        }
        return Ok(reponse);
    }
}
