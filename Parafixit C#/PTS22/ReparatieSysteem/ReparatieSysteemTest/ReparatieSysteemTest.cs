using ReparatieSysteem;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReparatieSysteemTest
{
    /// <summary>
    /// Summary description for ReparatieSysteemTest
    /// </summary>
    [TestClass]
    public class ReparatieSysteemTest
    {
        public ReparatieSysteemTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WijzigEnGetKlant1()
        {
            OracleDatabase db = new OracleDatabase();
            db.SaveKlant("Henk", "van", "Erp", "Eindhoven", "8844AK", "Hoofdstraat", 7, "henkvanerp@gmail.com", "0401234567", "Nederland");
            Klant klant1 = new Klant(1, "Henk", "van", "Erp", "Eindhoven", "8844AK", "Hoofdstraat", 8, "henkvanerp@gmail.com", "0401234567", "Nederland");
            db.WijzigKlant(klant1);
            int actual = db.GetKlantByKlantnr(1).Nr;
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WijzigEnGetKlant2()
        {
            OracleDatabase db = new OracleDatabase();
            db.SaveKlant("Henk", "van", "Erp", "Eindhoven", "8844AK", "Hoofdstraat", 7, "henkvanerp@gmail.com", "0401234567", "Nederland");
            Klant klant1 = new Klant(1, "Henk", "van", "Erp", "Eindhoven", "8844AK", "Hoofdstraat", 8, "henkvanerp@gmail.com", "0401234567", "Nederland");
            db.WijzigKlant(klant1);
            string actual = db.GetKlantByKlantnr(1).Voornaam;
            string expected = "Henk";
            Assert.AreEqual(expected, actual);
        }

        public void AddMedewerkerTest()
        {
            OracleDatabase db = new OracleDatabase();
            Medewerker willem = db.SaveMedewerker("Willem", "willempje", "0404040404", "Eindhoven", 2);
            Medewerker ingelogd = db.LogIn(Convert.ToInt32(willem.Mednr), "willempje");
            string expected = "Willem";
            string actual = ingelogd.Naam;
            Assert.AreEqual(expected, actual);          
        }
    }
}
