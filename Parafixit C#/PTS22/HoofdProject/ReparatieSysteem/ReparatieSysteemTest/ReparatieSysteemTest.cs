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
        public void TestMethod1()
        {
            int klantnr = 1;
            string voornaam = "Willem";
            string tussenvoegsel = " ";
            string achternaam = "Lodewijks";
            string woonplaats = "Eindhoven";
            string postcode = "3388AK";
            string straatnaam = "Steltlopen";
            int huisnummer = 77;
            string email = "Willemlodewijks@yahoo.nl";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant klant = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon);
            string expected = klant.Nr + ", " + klant.Voornaam + ", " + klant.Tussenvoegsel + ", " + klant.Achternaam + ", " + klant.Woonplaats;
            //string expected = "1, Willem, Lodewijks, Eindhoven";
            string actual = klant.ToString();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        
        public void GeenTussenvoegsel()
        {
            int klantnr = 1;
            string voornaam = "Willem";
            string tussenvoegsel = " ";
            string achternaam = "Lodewijks";
            string woonplaats = "Eindhoven";
            string postcode = "3388AK";
            string straatnaam = "Steltlopen";
            int huisnummer = 77;
            string email = "Willemlodewijks@yahoo.nl";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant klant = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon);
            //string expected = klant.Nr + ", " + klant.Voornaam + ", " + klant.Tussenvoegsel + ", " + klant.Achternaam + ", " + klant.Woonplaats;
            string expected = "1, Willem, Lodewijks, Eindhoven";
            string actual = klant.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
