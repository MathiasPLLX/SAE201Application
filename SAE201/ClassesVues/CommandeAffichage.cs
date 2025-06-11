using System.ComponentModel;

namespace SAE201.ClassesVues
{
    internal class CommandeAffichage : INotifyPropertyChanged
    {





        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
