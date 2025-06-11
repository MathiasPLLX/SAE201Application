using SAE201.Model;
using System.Collections.ObjectModel;

namespace SAE201.ClassesVues
{
    public class CommandeTableau
    {
        // Fix: Ensure the accessibility of CommandeAffichage matches the property accessibility  
        public ObservableCollection<CommandeAffichage> Commandes { get; set; }

        public CommandeTableau()
        {
            Commandes = new ObservableCollection<CommandeAffichage>(ToutesLesCommandes());
        }

        public List<CommandeAffichage> ToutesLesCommandes()
        {
            List<CommandeAffichage> liste = new List<CommandeAffichage>();

            List<Commande> commandesBdd = new Commande().FindAll();

            foreach (var commande in commandesBdd)
            {
                liste.Add(new CommandeAffichage
                {
                    NumCommande = commande.NumCommande,
                    DateCommande = commande.DateCommande ?? DateTime.MinValue,
                    CommandeValidee = commande.Valider.HasValue ? commande.Valider.Value : false,
                    PrixTotalCommande = (double)(commande.PrixTotal ?? 0m),
                    QuantiteCommande = commande.QuantiteCommande ?? 0,
                    NomVin = commande.NomVin ?? string.Empty
                });
            }

            return liste;
        }
    }
}