using immoBack.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace immoBack.Controllers;

[ApiController]
[Route("api/immobilier/authentification")]
[EnableCors("AllowSpecificOrigin")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;


    public AuthController(ILogger<AuthController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost("loginAdmin")]
    public IActionResult LoginAdmin([FromBody] UserModel user)
    {
        Reponse reponse=new Reponse();
        try{
            Utilisateur utilisateur = new Utilisateur{ email=user.email, mdp=user.mdp }.GetAdmin(null);
            Token token=new Token { utilisateur=utilisateur };
            string tokenUser = token.getToken(_configuration["Jwt:Key"]);
            Dictionary<string,string> data=new Dictionary<string,string>();
            data.Add("token",tokenUser);
            data.Add("role",utilisateur.role.intitule);
            reponse.data=data;

            // reponse.data=tokenUser;
        }catch(Exception e){
            reponse.erreur=e.Message;
        }
        return Ok(reponse);
    }

    [HttpPost("loginProprietaire")]
    public IActionResult LoginProprietaire([FromBody] UserModel user)
    {
        Reponse reponse=new Reponse();
        try{
            Utilisateur utilisateur = new Utilisateur{ contact=user.contact }.GetProprietaire(null);
            Token token=new Token { utilisateur=utilisateur };
            string tokenUser = token.getToken(_configuration["Jwt:Key"]);
            Dictionary<string,string> data=new Dictionary<string,string>();
            data.Add("token",tokenUser);
            data.Add("role",utilisateur.role.intitule);
            reponse.data=data;
        }catch(Exception e){
            reponse.erreur=e.Message;
        }
        return Ok(reponse);
    }

    [HttpPost("loginClient")]
    public IActionResult LoginClient([FromBody] UserModel user)
    {
        Reponse reponse=new Reponse();
        try{
            Utilisateur utilisateur = new Utilisateur{ email=user.email }.GetClient(null);
            Token token=new Token { utilisateur=utilisateur };
            string tokenUser = token.getToken(_configuration["Jwt:Key"]);
            Dictionary<string,string> data=new Dictionary<string,string>();
            data.Add("token",tokenUser);
            data.Add("role",utilisateur.role.intitule);
            reponse.data=data;
        }catch(Exception e){
            reponse.erreur=e.Message;
        }
        return Ok(reponse);
    }
}
