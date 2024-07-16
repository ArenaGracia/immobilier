using Npgsql;
using System;

namespace immoBack.Models;

public class Connect{
    public static NpgsqlConnection connectDB()
    {
        var server = "localhost";
        var port = "5433";
        var database = "immo_base";
        var username = "postgres";
        var password = "adminp15";

        var connString = $"Host={server};Port={port};Database={database};Username={username};Password={password};Encoding=UTF8";

        NpgsqlConnection conn = new NpgsqlConnection(connString);

        try{
            Console.WriteLine("Ouverture de la connexion...");
            conn.Open();
            Console.WriteLine("Connexion réussie !");
        }
        catch (Exception e){
            throw e;
        }
        return conn;
    }
}