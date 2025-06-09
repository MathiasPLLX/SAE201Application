using SAE201.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    NomAppelation = vin.Appelation?.NomAppelation, // sécurité si Appelation est null
                    NomType = vin.TypeVin?.NomType,                 // sécurité si TypeVin est null
                    PrixVin = vin.PrixVin ?? 0,                    // sécurité si PrixVin est null
                    Millesime = vin.Millesime ?? 0                 // sécurité si Millesime est null
                });
            }

            return liste;
        }
    }
}
