using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class Appelation
    {
        public int NumAppelation { get; set; }
        public string NomAppelation { get; set; }

        public List<Appelation> Appelations { get; set; }

        public List<Appelation> FindAll()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM APPELATION;");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        public List<Appelation> FindBySelection(string criteres)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM APPELATION WHERE nomappelation ILIKE @crit;");
            cmd.Parameters.AddWithValue("@crit", "%" + criteres + "%");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        private List<Appelation> ConvertToList(DataTable table)
        {
            var list = new List<Appelation>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Appelation
                {
                    NumAppelation = Convert.ToInt32(row["numappelation"]),
                    NomAppelation = row["nomappelation"].ToString()
                });
            }

            return list;
        }
    }
}
