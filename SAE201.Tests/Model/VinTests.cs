using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201.Model;
using System;
using System.Collections.Generic;

namespace SAE201.Tests.Model
{
    [TestClass]
    public class VinTests
    {
        #region Tests de propriétés numériques
        
        [TestMethod]
        public void NumVin_DefiniValeurValide_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            int valeurAttendue = 1;
            
            // Act
            vin.NumVin = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.NumVin);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumVin_DefiniValeurZero_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.NumVin = 0;
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumVin_DefiniValeurNegative_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.NumVin = -1;
        }
        
        [TestMethod]
        public void NumFournisseur_DefiniValeurValide_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            int valeurAttendue = 5;
            
            // Act
            vin.NumFournisseur = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.NumFournisseur);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumFournisseur_DefiniValeurZero_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.NumFournisseur = 0;
        }
        
        [TestMethod]
        public void NumType_DefiniValeurValide_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            int valeurAttendue = 3;
            
            // Act
            vin.NumType = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.NumType);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumType_DefiniValeurInvalide_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.NumType = -5;
        }
        
        [TestMethod]
        public void NumAppelation_DefiniValeurValide_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            int valeurAttendue = 10;
            
            // Act
            vin.NumAppelation = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.NumAppelation);
        }
        
        #endregion
        
        #region Tests des propriétés nullables
        
        [TestMethod]
        public void Millesime_DefiniValeurNull_DoitDefinirNull()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Millesime = null;
            
            // Assert
            Assert.IsNull(vin.Millesime);
        }
        
        [TestMethod]
        public void Millesime_DefiniValeur_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            int valeurAttendue = 2020;
            
            // Act
            vin.Millesime = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.Millesime);
        }
        
        [TestMethod]
        public void PrixVin_DefiniValeurNull_DoitDefinirNull()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.PrixVin = null;
            
            // Assert
            Assert.IsNull(vin.PrixVin);
        }
        
        [TestMethod]
        public void PrixVin_DefiniValeurPositive_DoitDefinirValeur()
        {
            // Arrange
            var vin = new Vin();
            decimal valeurAttendue = 29.99m;
            
            // Act
            vin.PrixVin = valeurAttendue;
            
            // Assert
            Assert.AreEqual(valeurAttendue, vin.PrixVin);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PrixVin_DefiniValeurNegative_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.PrixVin = -10.50m;
        }
        
        #endregion
        
        #region Tests des propriétés de type chaîne
        
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Descriptif_DefiniChaineVide_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Descriptif = "";
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Descriptif_DefiniEspacesBlancs_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Descriptif = "   ";
        }
        
        #endregion
        
        #region Tests des propriétés d'objets liés
        
        [TestMethod]
        public void Fournisseur_DefiniObjetValide_DoitDefinirObjet()
        {
            // Arrange
            var vin = new Vin();
            var fournisseur = new Fournisseur { NumFournisseur = 1, NomFournisseur = "Vignobles Dupont" };
            
            // Act
            vin.Fournisseur = fournisseur;
            
            // Assert
            Assert.AreEqual(fournisseur, vin.Fournisseur);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Fournisseur_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Fournisseur = null;
        }
        
        [TestMethod]
        public void TypeVin_DefiniObjetValide_DoitDefinirObjet()
        {
            // Arrange
            var vin = new Vin();
            var typeVin = new TypeVin { NumType = 1, NomType = "Rouge" };
            
            // Act
            vin.TypeVin = typeVin;
            
            // Assert
            Assert.AreEqual(typeVin, vin.TypeVin);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TypeVin_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.TypeVin = null;
        }
        
        [TestMethod]
        public void Appelation_DefiniObjetValide_DoitDefinirObjet()
        {
            // Arrange
            var vin = new Vin();
            var appelation = new Appelation { NumAppelation = 1, NomAppelation = "Bordeaux" };
            
            // Act
            vin.Appelation = appelation;
            
            // Assert
            Assert.AreEqual(appelation, vin.Appelation);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Appelation_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Appelation = null;
        }
        
        #endregion
        
        #region Tests des propriétés de collections
        
        [TestMethod]
        public void DetailsCommandes_DefiniListeValide_DoitDefinirListe()
        {
            // Arrange
            var vin = new Vin();
            var listeDetailsCommandes = new List<DetailCommande>();
            
            // Act
            vin.DetailsCommandes = listeDetailsCommandes;
            
            // Assert
            Assert.AreEqual(listeDetailsCommandes, vin.DetailsCommandes);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DetailsCommandes_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.DetailsCommandes = null;
        }
        
        [TestMethod]
        public void Demandes_DefiniListeValide_DoitDefinirListe()
        {
            // Arrange
            var vin = new Vin();
            var listeDemandes = new List<Demande>();
            
            // Act
            vin.Demandes = listeDemandes;
            
            // Assert
            Assert.AreEqual(listeDemandes, vin.Demandes);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Demandes_DefiniValeurNull_DoitLeverException()
        {
            // Arrange
            var vin = new Vin();
            
            // Act
            vin.Demandes = null;
        }
        
        #endregion
        
        #region Tests des propriétés calculées
        
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
        public void NomAppelation_AvecAppelationNull_DoitRetournerValeurParDefaut()
        {
            // Arrange
            var vin = new Vin();
            vin.GetType().GetProperty("Appelation").SetValue(vin, null);  // Contourne la validation pour le test
            
            // Act
            string resultat = vin.NomAppelation;
            
            // Assert
            Assert.AreEqual("Non définie", resultat);
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
        
        [TestMethod]
        public void NomType_AvecTypeVinNull_DoitRetournerValeurParDefaut()
        {
            // Arrange
            var vin = new Vin();
            vin.GetType().GetProperty("TypeVin").SetValue(vin, null);  // Contourne la validation pour le test
            
            // Act
            string resultat = vin.NomType;
            
            // Assert
            Assert.AreEqual("Non défini", resultat);
        }
        
        #endregion
    }
}