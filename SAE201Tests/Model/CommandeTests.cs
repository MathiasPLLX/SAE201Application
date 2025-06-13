using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201.Model;
using System;
using System.Collections.Generic;

namespace SAE201.Tests.Model
{
    [TestClass]
    public class CommandeTests
    {
        
        [TestMethod]
        public void NomVin_DefiniValeurValide_DoitDefinirValeur()
        {
            // Arrange
            var commande = new Commande();
            string valeurAttendue = "Château Margaux";
            
            // Act
            commande.NomVin = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, commande.NomVin);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NomVin_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var commande = new Commande();
            
            // Act
            commande.NomVin = null;
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NomVin_DefiniEspacesBlancs_DoitLeverException()
        {
            // Arrange
            var commande = new Commande();
            
            // Act
            commande.NomVin = " ";
        }
        
    }
}