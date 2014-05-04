/*using Substrate.Nbt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;

namespace XmlTester
{
    
    
    /// <summary>
    ///This is a test class for XmlDeserializerTest and is intended
    ///to contain all XmlDeserializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlDeserializerTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
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
        ///A test for XmlDeserializer Constructor
        ///</summary>
        [TestMethod()]
        public void XmlDeserializerConstructorTest() {
            XmlDeserializer target = new XmlDeserializer();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Deserialize
        ///</summary>
        [TestMethod()]
        public void DeserializeTest() {
            XmlDeserializer target = new XmlDeserializer(); // TODO: Initialize to an appropriate value
            XmlReader reader = null; // TODO: Initialize to an appropriate value
            TagNode expected = null; // TODO: Initialize to an appropriate value
            TagNode actual;
            actual = target.Deserialize(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Deserialize
        ///</summary>
        [TestMethod()]
        public void DeserializeTest1() {
            XmlDeserializer target = new XmlDeserializer(); // TODO: Initialize to an appropriate value
            Stream inputStream = null; // TODO: Initialize to an appropriate value
            TagNode expected = null; // TODO: Initialize to an appropriate value
            TagNode actual;
            actual = target.Deserialize(inputStream);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeserializeCompound
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeCompoundTest() {
            XmlDeserializer_Accessor target = new XmlDeserializer_Accessor(); // TODO: Initialize to an appropriate value
            XmlReader reader = null; // TODO: Initialize to an appropriate value
            Tuple<string, TagNode> expected = null; // TODO: Initialize to an appropriate value
            Tuple<string, TagNode> actual;
            actual = target.DeserializeCompound(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeserializeList
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeListTest() {
            XmlDeserializer_Accessor target = new XmlDeserializer_Accessor(); // TODO: Initialize to an appropriate value
            XmlReader reader = null; // TODO: Initialize to an appropriate value
            Tuple<string, TagNode> expected = null; // TODO: Initialize to an appropriate value
            Tuple<string, TagNode> actual;
            actual = target.DeserializeList(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeserializeScalar
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeScalarTest() {
            XmlDeserializer_Accessor target = new XmlDeserializer_Accessor(); // TODO: Initialize to an appropriate value
            XmlReader reader = null; // TODO: Initialize to an appropriate value
            Tuple<string, TagNode> expected = null; // TODO: Initialize to an appropriate value
            Tuple<string, TagNode> actual;
            actual = target.DeserializeScalar(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeserializeStart
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeStartTest() {
            XmlDeserializer_Accessor target = new XmlDeserializer_Accessor(); // TODO: Initialize to an appropriate value
            XmlReader reader = null; // TODO: Initialize to an appropriate value
            TagNode expected = null; // TODO: Initialize to an appropriate value
            TagNode actual;
            actual = target.DeserializeStart(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for readTextElement
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void readTextElementTest() {
            XmlDeserializer_Accessor target = new XmlDeserializer_Accessor(); // TODO: Initialize to an appropriate value
            XmlReader reader = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.readTextElement(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}*/
