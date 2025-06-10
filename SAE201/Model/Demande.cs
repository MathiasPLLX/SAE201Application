using Npgsql;
using System.Data;
using System.DirectoryServices.ActiveDirectory;

namespace SAE201.Model
{
    public class Demande
    {
        public int NumDemande { get; set; }

        public int NumVin { get; set; }
        public int NumEmploye { get; set; }
        public int NumCommande { get; set; }
        public int NumClient { get; set; }

        public DateTime? DateDemande { get; set; }
        public int? QuantiteDemande { get; set; }
        public string Accepter { get; set; }

        public Vin Vin { get; set; }
        public Employe Employe { get; set; }
        public Commande Commande { get; set; }
        public Client Client { get; set; }

        public int Create()
        {
            var cmd = new NpgsqlCommand("INSERT INTO DEMANDE(numdemande, numvin, numeploye, numcommande, numclient, datedemande, quantitedemande, accepter) VALUES (@numdemande, @numvin, @numemploye, @numcommande, @numclient, @datedemande, @quantitedemande, @accepter) RETURNING numdemande;");
            cmd.Parameters.AddWithValue("@numdemande", NumDemande);
            cmd.Parameters.AddWithValue("@numvin", NumVin);
            cmd.Parameters.AddWithValue("@numemploye", NumEmploye);
            cmd.Parameters.AddWithValue("@numcommande", NumCommande);
            cmd.Parameters.AddWithValue("@numclient", NumClient);
            cmd.Parameters.AddWithValue("@datedemande", DateDemande);
            cmd.Parameters.AddWithValue("@quantitedemande", QuantiteDemande);
            cmd.Parameters.AddWithValue("@accepter", Accepter);

            return NumDemande = DataAccess.Instance.ExecuteInsert(cmd);
        }

        public void Read()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM DEMANDE WHERE numdemande = @numdemande;");
            cmd.Parameters.AddWithValue("@numdemande", NumDemande);
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                NumDemande = Convert.ToInt32(row["numdemande"]);
                NumVin = Convert.ToInt32(row["numvin"]);
                NumEmploye = Convert.ToInt32(row["numemploye"]);
                NumCommande = Convert.ToInt32(row["numcommande"]);
                NumClient = Convert.ToInt32(row["numclient"]);
                DateDemande = Convert.ToDateTime(row["datedemande"]);
                QuantiteDemande = Convert.ToInt32(row["quantitedemande"]);
                Accepter = row["accepter"].ToString();
            }
        }

        public int Update()
        {
            var cmd = new NpgsqlCommand("UPDATE DEMANDE SET numvin = @numvin, numemploye = @numemploye, numcommande = @numcommande, numclient = @numclient, datedemande = @datedemande, quantitedemande = @quantitedemande, accetper = @accepter WHERE numdemande = @numdemande;");
            cmd.Parameters.AddWithValue("@numdemande", NumDemande);
            cmd.Parameters.AddWithValue("@numvin", NumVin);
            cmd.Parameters.AddWithValue("@numemploye", NumEmploye);
            cmd.Parameters.AddWithValue("@numcommande", NumCommande);
            cmd.Parameters.AddWithValue("@numclient", NumClient);
            cmd.Parameters.AddWithValue("@datedemande", DateDemande);
            cmd.Parameters.AddWithValue("@quantitedemande", QuantiteDemande);
            cmd.Parameters.AddWithValue("@accepter", Accepter);

            return DataAccess.Instance.ExecuteSet(cmd);
        }

        public int Delete()
        {
            var cmd = new NpgsqlCommand("DELETE FROM DEMANDE WHERE numdemande = @numdemande;");
            cmd.Parameters.AddWithValue("@numdemande", NumDemande);
            return DataAccess.Instance.ExecuteSet(cmd);
        }

        public List<Demande> FindAll()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM DEMANDE;");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        public List<Demande> FindBySelection(string criteres)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM DEMANDE WHERE numdemande ILIKE @crit;");
            cmd.Parameters.AddWithValue("@crit", "%" + criteres + "%");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        private List<Demande> ConvertToList(DataTable table)
        {
            var list = new List<Demande>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Demande
                {
                    NumDemande = Convert.ToInt32(row["numdemande"]),
                    NumVin = Convert.ToInt32(row["numvin"]),
                    NumEmploye = Convert.ToInt32(row["numemploye"]),
                    NumCommande = Convert.ToInt32(row["numcommande"]),
                    NumClient = Convert.ToInt32(row["numclient"]),
                    DateDemande = Convert.ToDateTime(row["datedemande"]),
                    QuantiteDemande = Convert.ToInt32(row["quantitedemande"]),
                    Accepter = row["accepter"].ToString()
                });
            }

            return list;
        }
    }
}
