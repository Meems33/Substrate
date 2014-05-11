using Substrate.Nbt;
using Substrate.ImportExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace XmlTester
{
    [TestClass()]
    public class XmlSerializerTest {
        private XmlWriter writer;
        private StringWriter output;
        private TestContext testContextInstance;
        private XmlSerializer target { get { return (XmlSerializer)wrapper.Target; } }
        private PrivateObject wrapper;

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
            output = new StringWriter();
            writer = XmlWriter.Create(output, createSettings());
            wrapper = new PrivateObject(typeof(XmlSerializer));
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (writer != null) {
                writer.Close();
            }

            if (output != null) {
                output.Close();
            }
        }
        
        #endregion

        private XmlWriterSettings createSettings() {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            return settings;
        }

        private string getResult() {
            writer.Flush();
            output.Flush();

            string rt = output.ToString();

            output.GetStringBuilder().Clear();
            writer = XmlWriter.Create(output, createSettings());

            return rt;
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeStartTest() {
            TagNode node;
            string result;
            string expected;

            // Scalar
            node = new TagNodeByte(2);
            expected = "<TAG_BYTE>2</TAG_BYTE>";

            SerializeStart(writer, node);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Compound
            node = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", new TagNodeDouble(4.3));
            
            expected = "<TAG_COMPOUND><TAG_DOUBLE name=\"name2\">4.3</TAG_DOUBLE></TAG_COMPOUND>";

            SerializeStart(writer, node);
            result = getResult();

            Assert.AreEqual(expected, result);

            // List
            node = new TagNodeList(TagType.TAG_SHORT);
            ((TagNodeList)node).Add(new TagNodeShort(3));
            expected = "<TAG_LIST type=\"TAG_SHORT\"><TAG_SHORT>3</TAG_SHORT></TAG_LIST>";

            SerializeStart(writer, node);
            result = getResult();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagEndTest() {
            // No Attribute
            doScalarTest(XmlTestScenario.TAG_END_1);

            // Attribute
            doScalarTest(XmlTestScenario.TAG_END_2);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagByteTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_BYTE_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_BYTE_2);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_BYTE_3);

            // Numerical Tests
            doScalarTest(XmlTestScenario.TAG_BYTE_4);

            // Hex Tests
            target.ByteAsHex = true;
            doScalarTest(XmlTestScenario.TAG_BYTE_5_H);
            doScalarTest(XmlTestScenario.TAG_BYTE_6_H);
            doScalarTest(XmlTestScenario.TAG_BYTE_7_H);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagShortTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_SHORT_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_SHORT_2);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_SHORT_3);

            // Numerical Tests
            doScalarTest(XmlTestScenario.TAG_SHORT_4);
            doScalarTest(XmlTestScenario.TAG_SHORT_5);

            // Hex Tests
            target.ShortAsHex = true;
            doScalarTest(XmlTestScenario.TAG_SHORT_6_H);
            doScalarTest(XmlTestScenario.TAG_SHORT_7_H);
            doScalarTest(XmlTestScenario.TAG_SHORT_8_H);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagIntTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_INT_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_INT_2);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_INT_3);

            // Numerical Tests
            doScalarTest(XmlTestScenario.TAG_INT_4);
            doScalarTest(XmlTestScenario.TAG_INT_5);

            // Hex Tests
            target.IntAsHex = true;
            doScalarTest(XmlTestScenario.TAG_INT_6_H);
            doScalarTest(XmlTestScenario.TAG_INT_7_H);
            doScalarTest(XmlTestScenario.TAG_INT_8_H);
            doScalarTest(XmlTestScenario.TAG_INT_9_H);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagLongTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_LONG_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_LONG_2);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_LONG_3);

            // Numerical Tests
            doScalarTest(XmlTestScenario.TAG_LONG_4);
            doScalarTest(XmlTestScenario.TAG_LONG_5);

            // Hex Tests
            target.LongAsHex = true;
            doScalarTest(XmlTestScenario.TAG_LONG_6_H);
            doScalarTest(XmlTestScenario.TAG_LONG_7_H);
            doScalarTest(XmlTestScenario.TAG_LONG_8_H);
            doScalarTest(XmlTestScenario.TAG_LONG_9_H);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagFloatTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_FLOAT_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_FLOAT_2);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_FLOAT_3);

            // Numerical Tests
            doScalarTest(XmlTestScenario.TAG_FLOAT_4);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagDoubleTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_DOUBLE_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_DOUBLE_2);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_DOUBLE_3);

            // Numerical Tests
            doScalarTest(XmlTestScenario.TAG_DOUBLE_4);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagByteArrayTest() {
            target.ArraySep = ' ';
            target.ByteArrayAsHex = false;

            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_2);
            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_3);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_4);

            // Hex Tests
            target.ByteArrayAsHex = true;
            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_5_H);
            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_6_H);

            doScalarTest(XmlTestScenario.TAG_BYTE_ARRAY_7);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagStringTest() {
            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_STRING_1);
            doScalarTest(XmlTestScenario.TAG_STRING_2);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_STRING_3);
            doScalarTest(XmlTestScenario.TAG_STRING_4);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_STRING_5);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagListTest() {
            // No Attribute, No Value
            doListTest(XmlTestScenario.TAG_LIST_1);
            doListTest(XmlTestScenario.TAG_LIST_2);

            // No Attribute, Yes Value
            doListTest(XmlTestScenario.TAG_LIST_3);

            // Yes Attribute, Yes Value
            doListTest(XmlTestScenario.TAG_LIST_4);

            // List with List inside
            doListTest(XmlTestScenario.TAG_LIST_5);
            doListTest(XmlTestScenario.TAG_LIST_6);

            // List with Compound inside
            doListTest(XmlTestScenario.TAG_LIST_7);
            doListTest(XmlTestScenario.TAG_LIST_8);

            doListTest(XmlTestScenario.TAG_LIST_9);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagCompoundTest() {
            // No Attribute, No Value
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_1);

            // No Attribute, Yes Value
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_2);

            // Yes Attribute, Yes Value
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_3);

            // Compound with List inside
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_4);
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_5);

            // Compound with Compound inside
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_6);
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_7);

            doCompoundTest(XmlTestScenario.TAG_COMPOUND_8);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagIntArrayTest() {
            target.ArraySep = ' ';
            target.IntArrayAsHex = false;

            // No Attribute, No Value
            doScalarTest(XmlTestScenario.TAG_INT_ARRAY_1);

            // No Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_INT_ARRAY_2);
            doScalarTest(XmlTestScenario.TAG_INT_ARRAY_3);

            // Yes Attribute, Yes Value
            doScalarTest(XmlTestScenario.TAG_INT_ARRAY_4);

            // Hex tests
            target.IntArrayAsHex = true;
            doScalarTest(XmlTestScenario.TAG_INT_ARRAY_5_H);
            doScalarTest(XmlTestScenario.TAG_INT_ARRAY_6_H);
        }

        private void doScalarTest(XmlTestScenario test) {
            string result;
            SerializeScalar(writer, test.Node, test.Name);
            result = getResult();
            Assert.AreEqual(test.DestXml, result);
        }

        private void doListTest(XmlTestScenario test) {
            string result;
            SerializeList(writer, test.Node, test.Name);
            result = getResult();
            Assert.AreEqual(test.DestXml, result);
        }

        private void doCompoundTest(XmlTestScenario test) {
            string result;
            SerializeCompound(writer, test.Node, test.Name);
            result = getResult();
            Assert.AreEqual(test.DestXml, result);
        }

        private void SerializeStart(XmlWriter writer, TagNode node) {
            wrapper.Invoke("SerializeStart", new object[] { writer, node });
        }

        private void SerializeList(XmlWriter writer, TagNode node, string name) {
            wrapper.Invoke("SerializeList", new object[] { writer, node, name });
        }

        private void SerializeCompound(XmlWriter writer, TagNode node, string name) {
            wrapper.Invoke("SerializeCompound", new object[] { writer, node, name });
        }

        private void SerializeScalar(XmlWriter writer, TagNode node, string name) {
            wrapper.Invoke("SerializeScalar", new object[] { writer, node, name });
        }
    }
}
