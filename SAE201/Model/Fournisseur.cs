namespace SAE201.Model
{
    public class Fournisseur
    {
        public int NumFournisseur { get; set; }
        public string NomFournisseur { get; set; }

        // Liste des vins fournis par un fournisseur
        public List<Vin> Vins { get; set; }
    }
}
