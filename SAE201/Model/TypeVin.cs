using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201.Model
{
    public class TypeVin
    {
        public int NumType { get; set; }
        public string NomType { get; set; }

        // Liste des vins de ce type
        public List<Vin> Vins { get; set; }
    }
}
