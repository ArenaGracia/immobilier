using Npgsql;

namespace immoBack.Models;

public class Role{

    public int idRole { get; set; }
    public string intitule { get; set; }

    public Role(int idRole,string intitule){
        this.idRole=idRole;
        this.intitule=intitule;
    }

    public static List<Role> GetRoles(NpgsqlConnection con=null)
    {
        List<Role> roles=new List<Role>();
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT * FROM role";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        int idRole=int.Parse(reader["id_role"].ToString());
                        string intitule=reader["intitule"].ToString();
                        Console.WriteLine(intitule);
                        roles.Add(new Role(idRole,intitule));
                    }
                }
            }
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return roles;
    }
}