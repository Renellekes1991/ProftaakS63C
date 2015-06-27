using ReparatieSysteem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ReparatieSysteemTest
{
    
    
    /// <summary>
    ///This is a test class for KlantTest and is intended
    ///to contain all KlantTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KlantTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Telefoon
        ///</summary>
        [TestMethod()]
        public void TelefoonTest()
        {
            int klantnr = 0;
            string voornaam = "Henk";
            string tussenvoegsel = "van";
            string achternaam = "Erp";
            string woonplaats = "Eindhoven";
            string postcode = "8844AK";
            string straatnaam = "Hoofdstraat";
            int huisnummer = 7;
            string email = "henkvanerp@gmail.com";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant target = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon); // TODO: Initialize to an appropriate value
            string expected = "0401234567"; // TODO: Initialize to an appropriate value
            string actual;
            target.Telefoon = expected;
            actual = target.Telefoon;
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for Land
        ///</summary>
        [TestMethod()]
        public void LandTest()
        {
            int klantnr = 0;
            string voornaam = "Henk";
            string tussenvoegsel = "van";
            string achternaam = "Erp";
            string woonplaats = "Eindhoven";
            string postcode = "8844AK";
            string straatnaam = "Hoofdstraat";
            int huisnummer = 7;
            string email = "henkvanerp@gmail.com";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant target = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon); // TODO: Initialize to an appropriate value
            string expected = "Nederland";
            string actual;
            target.Land = expected;
            actual = target.Land;
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for Email
        ///</summary>
        [TestMethod()]
        public void EmailTest()
        {
            int klantnr = 0;
            string voornaam = "Henk";
            string tussenvoegsel = "van";
            string achternaam = "Erp";
            string woonplaats = "Eindhoven";
            string postcode = "8844AK";
            string straatnaam = "Hoofdstraat";
            int huisnummer = 7;
            string email = "henkvanerp@gmail.com";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant target = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon); // TODO: Initialize to an appropriate value
            string expected = "henkvanerp@gmail.com";
            string actual;
            target.Email = expected;
            actual = target.Email;
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringGeenTussenvoegselTest()
        {
            int klantnr = 0;
            string voornaam = "Henk";
            string tussenvoegsel = "van";
            string achternaam = "";
            string woonplaats = "Eindhoven";
            string postcode = "8844AK";
            string straatnaam = "Hoofdstraat";
            int huisnummer = 7;
            string email = "henkvanerp@gmail.com";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant target = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon); // TODO: Initialize to an appropriate value
            string expected = "0, Henk, Erp, Eindhoven";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringMetTussenvoegselTest()
        {
            int klantnr = 0;
            string voornaam = "Henk";
            string tussenvoegsel = "van";
            string achternaam = "Erp";
            string woonplaats = "Eindhoven";
            string postcode = "8844AK";
            string straatnaam = "Hoofdstraat";
            int huisnummer = 7;
            string email = "henkvanerp@gmail.com";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant target = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon); // TODO: Initialize to an appropriate value
            string expected = "0, Henk, van, Erp, Eindhoven";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for Klant Constructor
        ///</summary>
        [TestMethod()]
        public void KlantConstructorTest()
        {
            int klantnr = 0;
            string voornaam = "Henk";
            string tussenvoegsel = "van";
            string achternaam = "Erp";
            string woonplaats = "Eindhoven";
            string postcode = "8844AK";
            string straatnaam = "Hoofdstraat";
            int huisnummer = 7;
            string email = "henkvanerp@gmail.com";
            string land = "Nederland";
            string telefoon = "0401234567";
            Klant target = new Klant(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, land, telefoon);
            bool gelukt = false;
            if (target.Voornaam != " ")
            {
                gelukt = true;
            }
            bool expected = true;
            bool actual = gelukt;
            Assert.AreEqual(expected, actual);
        }
    }
}
