using SAE201.Model;
using System.Collections.ObjectModel;

namespace SAE201.ClassesVues
{
    public class VinsTableau
    {
        public ObservableCollection<VinAffichage> Vins { get; set; }

        public VinsTableau()
        {
            Vins = new ObservableCollection<VinAffichage>(TousLesVins());
        }

        public List<VinAffichage> TousLesVins()
        {
            List<VinAffichage> liste = new List<VinAffichage>();

            List<Vin> vinsBdd = new Vin().FindAll();

            foreach (var vin in vinsBdd)
            {
                liste.Add(new VinAffichage
                {
                    NumVin = vin.NumVin,
                    NomVin = vin.NomVin,
                    NomAppelation = vin.Appelation?.NomAppelation,
                    NomType = vin.TypeVin?.NomType,
                    PrixVin = vin.PrixVin ?? 0,
                    Millesime = vin.Millesime ?? 0
                });
            }

            return liste;
        }
    }
}
