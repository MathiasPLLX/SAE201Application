using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201.Model.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        // Tests des constructeurs et propriétés
        [TestMethod]
        public void Constructeur_ParDefaut_InitialiseCorrectement()
        {
            // Arrange & Act
            var commande = new Commande();

            // Assert
            Assert.AreEqual(0, commande.NumCommande);
            Assert.AreEqual(0, commande.NumEmploye);
            Assert.IsNull(commande.DateCommande);
            Assert.IsNull(commande.Valider);
            Assert.IsNull(commande.PrixTotal);
            Assert.IsNotNull(commande.DetailsCommandes);
            Assert.AreEqual(0, commande.DetailsCommandes.Count);
        }

        [TestMethod]
        public void Constructeur_AvecNumero_InitialiseCorrectement()
        {
            // Arrange & Act
            int numCommande = 5;
            var commande = new Commande(numCommande);

            // Assert
            Assert.AreEqual(numCommande, commande.NumCommande);
        }

        [TestMethod]
        public void Constructeur_AvecParametres_InitialiseCorrectement()
        {
            // Arrange
            int numEmploye = 10;
            DateTime dateCommande = new DateTime(2024, 05, 15);
            bool valider = true;
            decimal prixTotal = 150.75m;

            // Act
            var commande = new Commande(numEmploye, dateCommande, valider, prixTotal);

            // Assert
            Assert.AreEqual(numEmploye, commande.NumEmploye);
            Assert.AreEqual(dateCommande, commande.DateCommande);
            Assert.AreEqual(valider, commande.Valider);
            Assert.AreEqual(prixTotal, commande.PrixTotal);
        }

        [TestMethod]
        public void CommandeValidee_RetourneBonneValeur()
        {
            // Arrange
            var commandeValidee = new Commande { Valider = true };
            var commandeNonValidee = new Commande { Valider = false };
            var commandeNonDefinie = new Commande();

            // Act & Assert
            Assert.AreEqual("Oui", commandeValidee.CommandeValidee);
            Assert.AreEqual("Non", commandeNonValidee.CommandeValidee);
            Assert.AreEqual("Non défini", commandeNonDefinie.CommandeValidee);
        }

        // Tests d'égalité et HashCode
        [TestMethod]
        public void Equals_MemeNumCommande_RetourneVrai()
        {
            // Arrange
            var commande1 = new Commande(42);
            var commande2 = new Commande(42);

            // Act & Assert
            Assert.AreEqual(commande1, commande2);
            Assert.AreEqual(commande1.GetHashCode(), commande2.GetHashCode());
        }

        [TestMethod]
        public void Equals_NumCommandeDifferents_RetourneFaux()
        {
            // Arrange
            var commande1 = new Commande(42);
            var commande2 = new Commande(43);

            // Act & Assert
            Assert.AreNotEqual(commande1, commande2);
            Assert.AreNotEqual(commande1.GetHashCode(), commande2.GetHashCode());
        }

        // Test ToString
        [TestMethod]
        public void ToString_FormatCorrect()
        {
            // Arrange
            var commande = new Commande
            {
                NumCommande = 42,
                DateCommande = new DateTime(2024, 5, 15),
                Valider = true,
                PrixTotal = 199.99m
            };

            // Act
            string result = commande.ToString();

            // Assert
            StringAssert.Contains(result, "Commande #42");
            StringAssert.Contains(result, "15/05/2024");
            StringAssert.Contains(result, "Validée");
            StringAssert.Contains(result, "199,99");
        }
    }

    [TestClass]
    public class CommandeIntegrationTests
    {
        private Commande testCommande;
        private int testNumCommande;

        [TestInitialize]
        public void Setup()
        {
            // Configure DataAccess pour pointer vers une base de test si nécessaire

            // Créer une commande de test temporaire
            testCommande = new Commande
            {
                NumEmploye = 1, // Utiliser un ID existant dans votre DB
                DateCommande = DateTime.Now,
                Valider = false,
                PrixTotal = 99.99m
            };

            // Créer la commande dans la base
            testNumCommande = testCommande.Create();
            testCommande.NumCommande = testNumCommande;
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Nettoyer: supprimer la commande de test
            try
            {
                if (testNumCommande > 0)
                {
                    testCommande.Delete();
                }
            }
            catch { /* Ignorer les erreurs de nettoyage */ }
        }

    }
}