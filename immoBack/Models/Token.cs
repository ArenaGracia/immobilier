using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace immoBack.Models;

public class Token{
    public Utilisateur? utilisateur { get; set; }
    public string valeur { get; set; }
    public DateTime dateExpiration { get; set; }

    public string? findByUtilisateur(NpgsqlConnection con=null){
        string? Token=null;
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT valeur FROM token_utilisateur WHERE id_utilisateur=@idUtilisateur AND date_Expiration > @date";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@idUtilisateur",this.utilisateur.idUtilisateur);
                command.Parameters.AddWithValue("@date",DateTime.Now);

                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        Token=reader.GetString(0);
                    }
                }
            }
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return Token;
    }

    public void insert(NpgsqlConnection con=null){
        bool estValid=false;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="INSERT INTO token_utilisateur VALUES(@idUser,@valeur,@expiration)";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@idUser",this.utilisateur.idUtilisateur);
                command.Parameters.AddWithValue("@expiration",this.dateExpiration);
                command.Parameters.AddWithValue("@valeur",this.valeur);
                command.ExecuteNonQuery();
            }
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
    }

    public void createToken(string keyJwt)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyJwt));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        string typeChamp = (this.utilisateur.email!=null) ? this.utilisateur.email : this.utilisateur.contact; 

        var claims = new[]
        {
            new Claim(ClaimTypes.Actor, this.utilisateur.idUtilisateur.ToString()),
            new Claim("TypeChamp", typeChamp),
            new Claim(ClaimTypes.Role, this.utilisateur.role.intitule)
        };

        DateTime expiration=DateTime.Now.AddDays(1);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        this.dateExpiration=expiration;
        this.valeur=new JwtSecurityTokenHandler().WriteToken(token);
        Console.WriteLine(this.valeur);
    }

    public string getToken(string keyJwt,NpgsqlConnection con=null){
        string token=null;
        try{
            token=findByUtilisateur(con);
            if(token.IsNullOrEmpty()){
                createToken(keyJwt);
                insert(con);
                token=this.valeur;
            }
        }catch(Exception e){
            throw e;
        }
        return token;
    }
}