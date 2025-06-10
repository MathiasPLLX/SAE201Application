namespace SAE201.Model
{
    public class Demande
    {
        public int NumDemande { get; set; }

        public int NumVin { get; set; }
        public int NumEmploye { get; set; }
        public int NumCommande { get; set; }
        public int NumClient { get; set; }

        public DateTime? DateDemande { get; set; }
        public int? QuantiteDemande { get; set; }
        public string Accepter { get; set; }

        public Vin Vin { get; set; }
        public Employe Employe { get; set; }
        public Commande Commande { get; set; }
        public Client Client { get; set; }
    }
}
