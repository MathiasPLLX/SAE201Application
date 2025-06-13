using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace SAE201.Model
{
    public class Client : ICrud<Client>
    {
        public int NumClient { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public string MailClient { get; set; }

        public int Create()
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(
                    "INSERT INTO client (nomclient, prenomclient, mailclient) " +
                    "VALUES (@nom, @prenom, @mail) RETURNING numclient"
                );
                cmd.Parameters.AddWithValue("@nom", NomClient);
                cmd.Parameters.AddWithValue("@prenom", PrenomClient);
                cmd.Parameters.AddWithValue("@mail", MailClient);

                // Exécute la commande via DataAccess
                NumClient = DataAccess.Instance.ExecuteInsert(cmd);
                return NumClient;
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur lors de la création du client.");
                throw;
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Client> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Client> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
