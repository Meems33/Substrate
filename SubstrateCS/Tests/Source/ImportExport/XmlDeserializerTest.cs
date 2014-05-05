using Substrate.Nbt;
using Substrate.ImportExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Xml;
using System.IO;
using System;

namespace XmlTester
{
    [TestClass()]
    public class XmlDeserializerTest {
        private XmlReader reader;
        private MemoryStream inputStream;
        private XmlDeserializer target { get { return (XmlDeserializer)wrapper.Target; } }
        private PrivateObject wrapper;
        private TestContext testContextInstance;

        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            inputStream = new MemoryStream();
            wrapper = new PrivateObject(typeof(XmlDeserializer));
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (inputStream != null) {
                inputStream.Close();
            }

            if (reader != null) {
                reader.Close();
            }
        }
        
        #endregion

        private XmlReaderSettings createSettings() {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreProcessingInstructions = true;

            return settings;
        }

        private void setInput(string str) {
            setInput(str, false);
        }

        private void setInput(string str, bool readOne) {
            if (reader != null) {
                reader.Close();
            }

            inputStream.SetLength(0);
            byte[] data = Encoding.UTF8.GetBytes(str);
            inputStream.Write(data, 0, data.Length);
            inputStream.Position = 0;

            reader = XmlReader.Create(inputStream, createSettings());

            if (readOne) {
                reader.Read();
            }
        }

        /// <summary>
        ///A test for readTextElement
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeTagEndTest() {
            string name;
            TagNode result;
            string input;

            input = "<TAG_END />";

            setInput(input, true);
            DeserializeScalar(reader, out result, out name);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TagNodeNull), result.GetType());
            Assert.IsNull(name);
        }

        private void DeserializeCompound(XmlReader reader, out TagNodeCompound tag, out string name) {
            object[] args = new object[] { reader, null, null };
            wrapper.Invoke("DeserializeCompound", args);
            tag = args[1] as TagNodeCompound;
            name = args[2] as string;
        }

        private void DeserializeList(XmlReader reader, out TagNodeList tag, out string name) {
            object[] args = new object[] { reader, null, null };
            wrapper.Invoke("DeserializeList", args);
            tag = args[1] as TagNodeList;
            name = args[2] as string;
        }

        private void DeserializeScalar(XmlReader reader, out TagNode tag, out string name) {
            object[] args = new object[] { reader, null, null };
            wrapper.Invoke("DeserializeScalar", args);
            tag = args[1] as TagNode;
            name = args[2] as string;
        }
    }
}
