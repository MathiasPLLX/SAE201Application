using SAE201.ClassesVues;
using SAE201.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

public class DemandesTableau : INotifyPropertyChanged
{
    public ObservableCollection<DemandeAffichage> Demandes { get; set; }

    private DemandeAffichage selectedDemande;
    public DemandeAffichage SelectedDemande
    {
        get => selectedDemande;
        set
        {
            selectedDemande = value;
            OnPropertyChanged(nameof(SelectedDemande));
        }
    }

    public DemandesTableau()
    {
        Demandes = new ObservableCollection<DemandeAffichage>(ToutesLesDemandes());
    }
    public List<DemandeAffichage> ToutesLesDemandes()
    {
        List<DemandeAffichage> liste = new List<DemandeAffichage>();

        List<Demande> demandebdd = new Demande().FindAll();

        foreach (var demande in demandebdd)
        {
            liste.Add(new DemandeAffichage
            {
                NumDemande = demande.NumDemande,
                NumVin = demande.NumVin,
                NumEmploye = demande.NumEmploye,
                NumCommande = demande.NumCommande,
                NumClient = demande.NumClient,
                DateDemande = (DateTime)demande.DateDemande,
                QuantiteDemande = (int)demande.QuantiteDemande,
                Accepter = demande.Accepter
            });
        }

        return liste;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}