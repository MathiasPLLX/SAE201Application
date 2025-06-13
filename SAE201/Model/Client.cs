using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace SAE201.Model
{
    public class Client : ICrud<Client>
    {
        private int numClient;
        private string nomClient, prenomClient, mailClient;

        public int NumClient
        {
            get
            {
                return this.numClient;
            }

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Le numéro de client doit être supérieur à zéro.");
                this.numClient = value;
            }
        }

        public string NomClient
        {
            get
            {
                return this.nomClient;
            }

            set
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value, "Le nom du client ne peut pas être vide.");
                this.nomClient = value;
            }
        }

        public string PrenomClient
        {
            get
            {
                return this.prenomClient;
            }

            set
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value, "Le prénom du client ne peut pas être vide.");
                this.prenomClient = value;
            }
        }

        public string MailClient
        {
            get
            {
                return this.mailClient;
            }

            set
            {
                this.mailClient = value;
            }
        }

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
