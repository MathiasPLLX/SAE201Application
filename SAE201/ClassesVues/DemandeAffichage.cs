using System.ComponentModel;

namespace SAE201.ClassesVues
{
    public class DemandeAffichage : INotifyPropertyChanged
    {
        private int numDemande, numVin, numEmploye, numCommande, numClient, quantiteDemande;
        private string accepter;
        private DateTime dateDemande;

        public int NumDemande
        {
            get
            {
                return this.numDemande;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de demande doit être supérieur à zéro.");
                if (this.numDemande != value)
                {
                    this.numDemande = value;
                    OnPropertyChanged(nameof(numDemande));
                }
            }
        }

        public int NumVin
        {
            get
            {
                return this.numVin;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de demande doit être supérieur à zéro.");
                if (this.numVin != value)
                {
                    this.numVin = value;
                    OnPropertyChanged(nameof(numVin));
                }
            }
        }

        public int NumEmploye
        {
            get
            {
                return this.numEmploye;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de demande doit être supérieur à zéro.");
                if (this.numEmploye != value)
                {
                    this.numEmploye = value;
                    OnPropertyChanged(nameof(numEmploye));
                }
            }
        }

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de demande doit être supérieur à zéro.");
                if (this.numCommande != value)
                {
                    this.numCommande = value;
                    OnPropertyChanged(nameof(numCommande));
                }
            }
        }

        public int NumClient
        {
            get
            {
                return this.numClient;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de demande doit être supérieur à zéro.");
                if (this.numClient != value)
                {
                    this.numClient = value;
                    OnPropertyChanged(nameof(numClient));
                }
            }
        }

        public DateTime DateDemande
        {
            get
            {
                return this.dateDemande;
            }
            set
            {
                if (this.dateDemande != value)
                {
                    this.dateDemande = value;
                    OnPropertyChanged(nameof(dateDemande));
                }
            }
        }

        public int QuantiteDemande
        {
            get
            {
                return this.quantiteDemande;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "La quantité demandée doit être supérieure à zéro.");
                if (this.quantiteDemande != value)
                {
                    this.quantiteDemande = value;
                    OnPropertyChanged(nameof(quantiteDemande));
                }
            }
        }

        public string Accepter
        {
            get
            {
                return this.accepter;
            }
            set
            {
                if (this.accepter != value)
                {
                    this.accepter = value;
                    OnPropertyChanged(nameof(accepter));
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
