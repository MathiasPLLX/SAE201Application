using System.ComponentModel;

namespace SAE201.ClassesVues
{
    internal class CommandeAffichage : INotifyPropertyChanged
    {
        private int numCommande, quantiteCommande;
        private DateTime dateCommande;
        private bool commandeValidee;
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
                if (this.numCommande != value)
                {
                    this.numCommande = value;
                    OnPropertyChanged(nameof(NumCommande));
                }
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
                if (this.quantiteCommande != value)
                {
                    this.quantiteCommande = value;
                    OnPropertyChanged(nameof(QuantiteCommande));
                }
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
                if (this.dateCommande != value)
                {
                    this.dateCommande = value;
                    OnPropertyChanged(nameof(DateCommande));
                }
            }
        }

        public bool CommandeValidee
        {
            get
            {
                return this.commandeValidee;
            }

            set
            {
                if (this.commandeValidee != value)
                {
                    this.commandeValidee = value;
                    OnPropertyChanged(nameof(CommandeValidee));
                }
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
                if (this.prixTotalCommande != value)
                {
                    this.prixTotalCommande = value;
                    OnPropertyChanged(nameof(PrixTotalCommande));
                }
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
                if (this.nomVin != value)
                {
                    this.nomVin = value;
                    OnPropertyChanged(nameof(NomVin));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
