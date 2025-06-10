using SAE201.Model;
using System.Collections.ObjectModel;

namespace SAE201.ClassesVues
{
    public class DemandesTableau
    {
        public ObservableCollection<DemandeAffichage> Demandes { get; set; }

        public DemandesTableau()
        {
            Demandes = new ObservableCollection<VinAffichage>(TousLesVins());
        }

        public List<DemandeAffichage> TousLesVins()
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
                    DateDemande = demande.DateDemande,
                    QuantiteDemande = demande.QuantiteDemande,
                    Accepter = demande.Accepter
                });
            }

            return liste;
        }
    }
}
