using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201.Model
{
    public class Fournisseur
    {
        public int NumFournisseur { get; set; }
        public string NomFournisseur { get; set; }

        // Liste des vins fournis par un fournisseur
        public List<Vin> Vins { get; set; }
    }
}
