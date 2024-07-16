using immoBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace immoBack.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowSpecificOrigin")]
[Authorize(Policy = "proprietairePolicy")]
public class ProprietaireController : ControllerBase
{

    private readonly ILogger<ProprietaireController> _logger;
    private readonly IConfiguration _configuration;


    public ProprietaireController(ILogger<ProprietaireController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("Proprietaire-chiffre")]
    public IActionResult GetProprietaireData(DateOnly debut,DateOnly fin)
    {
        Reponse reponse=new Reponse();
        var token = HttpContext.Items["BearerToken"] as string;
        try{
            int idUtilisateur=Utilisateur.GetUtilisateurByToken(token);
            double valeur=VChiffreAffaire.GetChiffreProprietaire(debut,fin,idUtilisateur);
            reponse.data=valeur;
            Console.WriteLine(token);
        }catch(Exception e){
            reponse.erreur=e.Message;
        }
        return Ok(reponse);
    }
}
