using System.ComponentModel;

namespace SAE201.ClassesVues
{
    public class VinAffichage : INotifyPropertyChanged
    {
        private int numVin;
        private string nomVin;
        private string nomAppelation;
        private string nomType;
        private decimal prixVin;
        private int millesime;
        private int numFournisseur;
        private int numType;
        private int numAppelation;
        private string descriptif;

        public int NumVin
        {
            get
            {
                return this.numVin;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de vin doit être supérieur à zéro.");
                if (this.numVin != value)
                {
                    this.numVin = value;
                    OnPropertyChanged(nameof(NumVin));
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
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value, "Le nom du vin ne peut pas être vide.");
                if (this.nomVin != value)
                {
                    this.nomVin = value;
                    OnPropertyChanged(nameof(NomVin));
                }
            }
        }

        public string NomAppelation
        {
            get
            { 
                return this.nomAppelation; 
            }
            set
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value, "Le nom de l'appelation ne peut pas être vide.");
                if (this.nomAppelation != value)
                {
                    this.nomAppelation = value;
                    OnPropertyChanged(nameof(NomAppelation));
                }
            }
        }

        public string NomType
        {
            get 
            {
                return this.nomType; 
            }
            set
            {
                if (this.nomType != value)
                {
                    this.nomType = value;
                    OnPropertyChanged(nameof(NomType));
                }
            }
        }

        public decimal PrixVin
        {
            get 
            {
                return this.prixVin;
            }
            set
            {
                if (this.prixVin != value)
                {
                    this.prixVin = value;
                    OnPropertyChanged(nameof(PrixVin));
                }
            }
        }

        public int Millesime
        {
            get 
            { 
                return this.millesime; 
            }
            set
            {
                if (this.millesime != value)
                {
                    this.millesime = value;
                    OnPropertyChanged(nameof(Millesime));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int NumFournisseur
        {
            get
            {
                return this.numFournisseur;
            }
            set
            {
                if (this.numFournisseur != value)
                {
                    this.numFournisseur = value;
                    OnPropertyChanged(nameof(NumFournisseur));
                }
            }
        }

        public int NumType
        {
            get
            {
                return this.numType;
            }
            set
            {
                if (this.numType != value)
                {
                    this.numType = value;
                    OnPropertyChanged(nameof(NumType));
                }
            }
        }

        public int NumAppelation
        {
            get
            {
                return this.numAppelation;
            }
            set
            {
                if (this.numAppelation != value)
                {
                    this.numAppelation = value;
                    OnPropertyChanged(nameof(NumAppelation));
                }
            }
        }

        public string Descriptif
        {
            get
            {
                return this.descriptif;
            }
            set
            {
                if (this.descriptif != value)
                {
                    this.descriptif = value;
                    OnPropertyChanged(nameof(Descriptif));
                }
            }
        }
    }
}
