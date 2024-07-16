namespace immoBack.Models;

public class Reponse{

    public object? erreur { get; set; }
    public object? data { get; set; }

    public Reponse(){}
    public Reponse(object? erreur,object? data){
        this.erreur=erreur;
        this.data=data;
    }
}