using System.Data;
using Npgsql;

namespace immoBack.Models;

public class VChiffreAffaire{

    public string mois { get; set; }
    public int an { get;set; }
    public double chiffreAffaire { get; set; }
    public double gain { get; set; }

    public VChiffreAffaire(string mois, int an,double chiffreAffaire,double gain){
        this.mois=mois;
        this.an=an;
        this.chiffreAffaire=chiffreAffaire;
        this.gain=gain;
    }

    public static List<VChiffreAffaire> GetChiffreAffaireAdmin(DateOnly debut,DateOnly fin,NpgsqlConnection con=null)
    {
        List<VChiffreAffaire> VChiffreAffaires=new List<VChiffreAffaire>();
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT * FROM get_location_mois(@debut,@fin)";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@debut",debut);
                command.Parameters.AddWithValue("@fin",fin);
                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        string mois=reader.GetString(0);
                        Console.WriteLine(mois);
                        int an=int.Parse(reader["an"].ToString());
                        double chiffreAffaire=double.Parse(reader["chiffre_affaire"].ToString());
                        double gain=double.Parse(reader["gain"].ToString());
                        VChiffreAffaires.Add(new VChiffreAffaire(mois,an,chiffreAffaire,gain));
                    }
                }
            }
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return VChiffreAffaires;
    }

    public static double GetChiffreProprietaire(DateOnly debut,DateOnly fin,int proprietaire,NpgsqlConnection con=null)
    {
        double VChiffreAffaires=0;
        bool estValid=false;

        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql="SELECT * FROM get_chiffre_proprietaire(@debut,@fin,@proprietaire)";
            using (NpgsqlCommand command=new NpgsqlCommand(sql,con)){
                command.Parameters.AddWithValue("@debut",debut);
                command.Parameters.AddWithValue("@fin",fin);
                command.Parameters.AddWithValue("@proprietaire",proprietaire);
                using(NpgsqlDataReader reader=command.ExecuteReader()){
                    while (reader.Read()){
                        VChiffreAffaires=reader.GetDouble(0);
                    }
                }
            }
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid) con.Close();
        }
        return VChiffreAffaires;
    }
}