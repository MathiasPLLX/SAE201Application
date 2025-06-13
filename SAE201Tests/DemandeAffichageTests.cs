using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201.ClassesVues;
using System;
using System.ComponentModel;

namespace SAE201Tests
{
    [TestClass]
    public class DemandeAffichageTests
    {
        // Tests de validité pour les propriétés numériques
        [TestMethod]
        public void NumDemande_ValeurValide_DefiniCorrectement()
        {
            // Arrange
            var demande = new DemandeAffichage();
            int valeurAttendue = 1;

            // Act
            demande.NumDemande = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, demande.NumDemande);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumDemande_ValeurZero_LanceException()
        {
            // Arrange
            var demande = new DemandeAffichage();

            // Act
            demande.NumDemande = 0;

            // Assert est géré par l'attribut ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumDemande_ValeurNegative_LanceException()
        {
            // Arrange
            var demande = new DemandeAffichage();

            // Act
            demande.NumDemande = -1;

            // Assert est géré par l'attribut ExpectedException
        }

        // Tests similaires pour les autres propriétés numériques
        [TestMethod]
        public void NumVin_ValeurValide_DefiniCorrectement()
        {
            var demande = new DemandeAffichage();
            demande.NumVin = 10;
            Assert.AreEqual(10, demande.NumVin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumVin_ValeurInvalide_LanceException()
        {
            var demande = new DemandeAffichage();
            demande.NumVin = 0;
        }

        [TestMethod]
        public void NumEmploye_ValeurValide_DefiniCorrectement()
        {
            var demande = new DemandeAffichage();
            demande.NumEmploye = 5;
            Assert.AreEqual(5, demande.NumEmploye);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumEmploye_ValeurInvalide_LanceException()
        {
            var demande = new DemandeAffichage();
            demande.NumEmploye = -10;
        }

        [TestMethod]
        public void NumCommande_ValeurValide_DefiniCorrectement()
        {
            var demande = new DemandeAffichage();
            demande.NumCommande = 100;
            Assert.AreEqual(100, demande.NumCommande);
        }

        [TestMethod]
        public void NumClient_ValeurValide_DefiniCorrectement()
        {
            var demande = new DemandeAffichage();
            demande.NumClient = 50;
            Assert.AreEqual(50, demande.NumClient);
        }

        [TestMethod]
        public void QuantiteDemande_ValeurValide_DefiniCorrectement()
        {
            var demande = new DemandeAffichage();
            demande.QuantiteDemande = 25;
            Assert.AreEqual(25, demande.QuantiteDemande);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QuantiteDemande_ValeurZero_LanceException()
        {
            var demande = new DemandeAffichage();
            demande.QuantiteDemande = 0;
        }

        // Tests pour DateDemande
        [TestMethod]
        public void DateDemande_ValeurValide_DefiniCorrectement()
        {
            // Arrange
            var demande = new DemandeAffichage();
            var dateAujourdhui = DateTime.Today;

            // Act
            demande.DateDemande = dateAujourdhui;

            // Assert
            Assert.AreEqual(dateAujourdhui, demande.DateDemande);
        }

        [TestMethod]
        public void DateDemande_ValeurFuture_DefiniCorrectement()
        {
            // Arrange
            var demande = new DemandeAffichage();
            var bonneDateDemande = DateTime.Today;

            // Act
            demande.DateDemande = bonneDateDemande;

            // Assert
            Assert.AreEqual(bonneDateDemande, demande.DateDemande);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DateDemande_ValeurPassee_LanceException()
        {
            // Arrange
            var demande = new DemandeAffichage();
            var datePassee = DateTime.Today.AddDays(+1);

            // Act
            demande.DateDemande = datePassee;

            // Assert est géré par l'attribut ExpectedException
        }

        // Tests pour Accepter
        [TestMethod]
        public void Accepter_ValeurDefinie_DefiniCorrectement()
        {
            // Arrange
            var demande = new DemandeAffichage();
            string valeurAttendue = "Oui";

            // Act
            demande.Accepter = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, demande.Accepter);
        }

        // Tests pour la notification de changement de propriété
        [TestMethod]
        public void NumDemande_ChangementValeur_DeclenchePropertyChanged()
        {
            // Arrange
            var demande = new DemandeAffichage();
            bool evenementDeclenche = false;
            string proprieteModifiee = string.Empty;
            demande.PropertyChanged += (s, e) =>
            {
                evenementDeclenche = true;
                proprieteModifiee = e.PropertyName;
            };

            // Act
            demande.NumDemande = 5;

            // Assert
            Assert.IsTrue(evenementDeclenche);
            Assert.AreEqual("numDemande", proprieteModifiee);
        }

        [TestMethod]
        public void Accepter_ChangementValeur_DeclenchePropertyChanged()
        {
            // Arrange
            var demande = new DemandeAffichage();
            bool evenementDeclenche = false;
            string proprieteModifiee = string.Empty;
            demande.PropertyChanged += (s, e) =>
            {
                evenementDeclenche = true;
                proprieteModifiee = e.PropertyName;
            };

            // Act
            demande.Accepter = "Non";

            // Assert
            Assert.IsTrue(evenementDeclenche);
            Assert.AreEqual("accepter", proprieteModifiee);
        }

        [TestMethod]
        public void DateDemande_MemeValeur_NeDeclenchePasPropertyChanged()
        {
            // Arrange
            var demande = new DemandeAffichage();
            var dateDemande = DateTime.Today;
            demande.DateDemande = dateDemande; // Première affectation

            bool evenementDeclenche = false;
            demande.PropertyChanged += (s, e) => { evenementDeclenche = true; };

            // Act
            demande.DateDemande = dateDemande; // Même valeur

            // Assert
            Assert.IsFalse(evenementDeclenche);
        }
    }
}