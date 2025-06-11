
namespace SAE201.Model
{
    public class Commande : ICrud<Commande>
    {
        private int numCommande;
        private DateTime dateCommande;
        private bool valider;
        private int quantiteCommande;
        private double prixTotalCommande;
        private string nomVin;

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return this.dateCommande;
            }

            set
            {
                this.dateCommande = value;
            }
        }

        public bool Valider
        {
            get
            {
                return this.valider;
            }

            set
            {
                this.valider = value;
            }
        }

        public int QuantiteCommande
        {
            get
            {
                return this.quantiteCommande;
            }

            set
            {
                this.quantiteCommande = value;
            }
        }

        public double PrixTotalCommande
        {
            get
            {
                return this.prixTotalCommande;
            }

            set
            {
                this.prixTotalCommande = value;
            }
        }

        public string NomVin
        {
            get
            {
                return this.nomVin;
            }

            set
            {
                this.nomVin = value;
            }
        }

        public int Create()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Commande> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Commande> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
