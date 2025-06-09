using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201.Model
{
    public class Vin
    {
        public int NumVin { get; set; }
        public int NumFournisseur { get; set; }
        public int NumType { get; set; }
        public int NumAppelation { get; set; }

        public string NomVin { get; set; }
        public decimal? PrixVin { get; set; }
        public string Descriptif { get; set; }
        public int? Millesime { get; set; }

        public Fournisseur Fournisseur { get; set; }
        public TypeVin TypeVin { get; set; }
        public Appelation Appelation { get; set; }

        // Pour voir où le vin est demandé ou commandé
        public List<DetailCommande> DetailsCommandes { get; set; }
        public List<Demande> Demandes { get; set; }
    }
}
