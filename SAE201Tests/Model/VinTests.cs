using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201.Model;
using System;
using System.Collections.Generic;

namespace SAE201.Tests.Model
{
    [TestClass]
    public class VinTests
    {
        
        [TestMethod]
        public void NomVin_DefiniValeur_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            string valeurAttendue = "Château Margaux";
            
            // Act
            vin.NomVin = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.NomVin);
        }
        
        [TestMethod]
        public void Descriptif_DefiniValeurValide_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            string valeurAttendue = "Un vin rouge corsé avec des notes de fruits noirs";
            
            // Act
            vin.Descriptif = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.Descriptif);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Descriptif_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Descriptif = null;
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Descriptif_DefiniChaineVide_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Descriptif = "";
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Descriptif_DefiniEspacesBlancs_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Descriptif = "   ";
        }
        
        [TestMethod]
        public void NomAppelation_AvecAppelationValide_DoitRetournerNom()
        {
            // Arrange
            var vin = new Vin();
            var appelation = new Appelation { NumAppelation = 1, NomAppelation = "Bordeaux" };
            vin.Appelation = appelation;
            
            // Act
            string resultat = vin.NomAppelation;
            
            // Assert
            Assert.AreEqual("Bordeaux", resultat);
        }
        
        
        [TestMethod]
        public void NomType_AvecTypeVinValide_DoitRetournerNom()
        {
            // Arrange
            var vin = new Vin();
            var typeVin = new TypeVin { NumType = 1, NomType = "Rouge" };
            vin.TypeVin = typeVin;
            
            // Act
            string resultat = vin.NomType;
            
            // Assert
            Assert.AreEqual("Rouge", resultat);
        }
        
    }
}