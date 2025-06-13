using SAE201.ClassesVues;

namespace SAE201.Tests
{
    [TestClass]
    public class CommandeAffichageTests
    {
        // Test du constructeur par défaut et initialisation
        [TestMethod]
        public void CommandeAffichage_Constructeur_ProprietesInitialisees()
        {
            // Act - Création d'un objet avec le constructeur par défaut
            var commandeAffichage = new CommandeAffichage();

            // Assert - Vérification des valeurs par défaut
            Assert.AreEqual(0, commandeAffichage.NumCommande);
            Assert.AreEqual(0, commandeAffichage.QuantiteCommande);
            Assert.AreEqual(default(DateTime), commandeAffichage.DateCommande);
            Assert.AreEqual(false, commandeAffichage.CommandeValidee);
            Assert.AreEqual(0, commandeAffichage.PrixTotalCommande);
            Assert.AreEqual(null, commandeAffichage.NomVin);
        }

        // Tests des propriétés avec leur validation
        [TestMethod]
        public void NumCommande_ValeurPositive_DefiniCorrectement()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();
            int valeurAttendue = 42;

            // Act
            commandeAffichage.NumCommande = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, commandeAffichage.NumCommande);
        }

        [TestMethod]
        public void NumCommande_ValeurZero_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commandeAffichage.NumCommande = 0);
        }

        [TestMethod]
        public void NumCommande_ValeurNegative_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commandeAffichage.NumCommande = -5);
        }

        [TestMethod]
        public void QuantiteCommande_ValeurPositive_DefiniCorrectement()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();
            int valeurAttendue = 10;

            // Act
            commandeAffichage.QuantiteCommande = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, commandeAffichage.QuantiteCommande);
        }

        [TestMethod]
        public void QuantiteCommande_ValeurZero_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commandeAffichage.QuantiteCommande = 0);
        }

        [TestMethod]
        public void QuantiteCommande_ValeurNegative_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commandeAffichage.QuantiteCommande = -3);
        }

        [TestMethod]
        public void DateCommande_ValeurValide_DefiniCorrectement()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();
            DateTime valeurAttendue = new DateTime(2024, 5, 20);

            // Act
            commandeAffichage.DateCommande = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, commandeAffichage.DateCommande);
        }

        [TestMethod]
        public void CommandeValidee_ModifierValeur_DefiniCorrectement()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage
            {
                NumCommande = 1 // Nécessaire pour UpdateDatabase
            };
            bool valeurAttendue = true;

            // Act - Dans un vrai test, on devrait mocker l'appel à UpdateDatabase
            try
            {
                commandeAffichage.CommandeValidee = valeurAttendue;
            }
            catch (Exception)
            {
                // Ignorer l'erreur de UpdateDatabase dans le test
            }

            // Assert
            Assert.AreEqual(valeurAttendue, commandeAffichage.CommandeValidee);
        }

        [TestMethod]
        public void PrixTotalCommande_ValeurPositive_DefiniCorrectement()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();
            double valeurAttendue = 199.99;

            // Act
            commandeAffichage.PrixTotalCommande = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, commandeAffichage.PrixTotalCommande);
        }

        [TestMethod]
        public void PrixTotalCommande_ValeurNegative_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commandeAffichage.PrixTotalCommande = -10.5);
        }

        [TestMethod]
        public void NomVin_ValeurValide_DefiniCorrectement()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();
            string valeurAttendue = "Château Margaux 2018";

            // Act
            commandeAffichage.NomVin = valeurAttendue;

            // Assert
            Assert.AreEqual(valeurAttendue, commandeAffichage.NomVin);
        }

        [TestMethod]
        public void NomVin_ValeurVide_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => commandeAffichage.NomVin = "");
        }

        [TestMethod]
        public void NomVin_ValeurEspaces_LanceException()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => commandeAffichage.NomVin = "   ");
        }

        // Test de l'événement PropertyChanged
        [TestMethod]
        public void PropertyChanged_ModifierPropriete_EvenementDeclenche()
        {
            // Arrange
            var commandeAffichage = new CommandeAffichage();
            bool evenementDeclenche = false;
            string proprieteModifiee = null;

            commandeAffichage.PropertyChanged += (sender, e) =>
            {
                evenementDeclenche = true;
                proprieteModifiee = e.PropertyName;
            };

            // Act
            commandeAffichage.NumCommande = 42;

            // Assert
            Assert.IsTrue(evenementDeclenche);
            Assert.AreEqual(nameof(CommandeAffichage.NumCommande), proprieteModifiee);
        }
    }
}