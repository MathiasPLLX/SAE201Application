namespace SAE201.Model
{
    public class Commande
    {
        public int NumCommande { get; set; }
        public int NumEmploye { get; set; }
        public DateTime? DateCommande { get; set; }
        public bool? Valider { get; set; }
        public decimal? PrixTotal { get; set; }

        public Employe Employe { get; set; }
    }
}
