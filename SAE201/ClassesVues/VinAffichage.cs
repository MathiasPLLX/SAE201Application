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

        public int NumVin
        {
            get
            {
                return this.numVin;
            }
            set
            {
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
    }
}
