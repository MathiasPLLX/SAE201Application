namespace SAE201.Model
{
    public class TypeVin
    {
        public int NumType { get; set; }
        public string NomType { get; set; }

        // Liste des vins de ce type
        public List<Vin> Vins { get; set; }
    }
}
