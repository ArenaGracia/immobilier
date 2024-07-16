using Npgsql;

namespace immoBack.Models;

public class Utilisateur{

    public int idUtilisateur { get; set; }
    public string nom { get; set; }
    public string email { get; set; }
    public string mdp { get; set; }
    public string contact { get; set; }
    public Role role { get; set; }
    public Utilisateur GetAdmin(NpgsqlConnection con=null)
    {
        Utilisateur utilisateur=null;
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT * FROM v_utilisateur WHERE email=@email AND mdp=@mdp  AND id_role=1";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@email",this.email);
                command.Parameters.AddWithValue("@mdp",this.mdp);

                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        int idRole=int.Parse(reader["id_role"].ToString());
                        string intitule=reader["intitule"].ToString();

                        utilisateur=new Utilisateur{
                            idUtilisateur=int.Parse(reader["id_utilisateur"].ToString()),
                            nom=reader["nom"].ToString(),
                            email=reader["email"].ToString(),
                            role=new Role(idRole,intitule)
                        };
                    }
                }
            }
            if(utilisateur==null) throw new Exception("Vérifier vos identifiants");
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return utilisateur;
    }

    public Utilisateur GetProprietaire(NpgsqlConnection con=null)
    {
        Utilisateur utilisateur=null;
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT * FROM v_utilisateur WHERE contact=@contact AND id_role=2";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@contact",this.contact);

                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        int idRole=int.Parse(reader["id_role"].ToString());
                        string intitule=reader["intitule"].ToString();

                        utilisateur=new Utilisateur{
                            idUtilisateur=int.Parse(reader["id_utilisateur"].ToString()),
                            contact=reader["contact"].ToString(),
                            role=new Role(idRole,intitule)
                        };
                    }
                }
            }
            if(utilisateur==null) throw new Exception("Vérifier vos identifiants propriétaire");
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return utilisateur;
    }

    public static int GetUtilisateurByToken(string token,NpgsqlConnection con=null)
    {
        int idUtilisateur=0;
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT id_utilisateur FROM token_utilisateur WHERE valeur=@valeur";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@valeur",token);

                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        idUtilisateur=reader.GetInt32(0);
                    }
                }
            }
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return idUtilisateur;
    }


    public Utilisateur GetClient(NpgsqlConnection con=null)
    {
        Utilisateur utilisateur=null;
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT * FROM v_utilisateur WHERE email=@email  AND id_role=3";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@email",this.email);

                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        int idRole=int.Parse(reader["id_role"].ToString());
                        string intitule=reader["intitule"].ToString();

                        utilisateur=new Utilisateur{
                            idUtilisateur=int.Parse(reader["id_utilisateur"].ToString()),
                            email=reader["email"].ToString(),
                            role=new Role(idRole,intitule)
                        };
                    }
                }
            }
            if(utilisateur==null) throw new Exception("Vérifier vos identifiants client");
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return utilisateur;
    }
}