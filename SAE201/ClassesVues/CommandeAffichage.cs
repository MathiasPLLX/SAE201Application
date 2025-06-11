using System.ComponentModel;

namespace SAE201.ClassesVues
{
    public class CommandeAffichage : INotifyPropertyChanged
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
                //ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(0, value, "NumCommande ne peut être ni négatif ni 0.");
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
                //ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(0, value, "QuantiteCommande ne peut être ni négatif ni 0.");
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
                //ArgumentNullException.ThrowIfNull(value, "DateCommande ne peut être null.");
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
                //ArgumentNullException.ThrowIfNull(value, "CommandeValidee ne peut être null.");
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
                //ArgumentOutOfRangeException.ThrowIfLessThan(0, value,"Prix impossible");
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
                //ArgumentException.ThrowIfNullOrWhiteSpace(value, "NomVin ne peut être null.");
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
