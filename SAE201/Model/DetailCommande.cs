using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201.Model
{
    public class DetailCommande
    {
        public int NumCommande { get; set; }
        public int NumVin { get; set; }

        public int? Quantite { get; set; }
        public decimal? Prix { get; set; }

        public Commande Commande { get; set; }
        public Vin Vin { get; set; }
    }
}
