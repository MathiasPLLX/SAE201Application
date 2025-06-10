namespace SAE201.Model
{
    public class Role
    {
        public int NumRole { get; set; }
        public string NomRole { get; set; }

        // Liste d'employés ayant ce rôle
        public List<Employe> Employes { get; set; }
    }
}
