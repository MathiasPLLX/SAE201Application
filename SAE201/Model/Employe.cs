namespace SAE201.Model
{
    public class Employe
    {
        public int NumEmploye { get; set; }
        public int NumRole { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }

        public Role Role { get; set; }
    }
}
