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

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            output = new StringWriter();
            writer = XmlWriter.Create(output, createSettings());
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
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string result;
            string expected;

            // Scalar
            node = new TagNodeByte(2);
            expected = "<TAG_BYTE>2</TAG_BYTE>";

            target.SerializeStart(writer, node);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Compound
            node = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", new TagNodeDouble(4.3));
            
            expected = "<TAG_COMPOUND><TAG_DOUBLE name=\"name2\">4.3</TAG_DOUBLE></TAG_COMPOUND>";

            target.SerializeStart(writer, node);
            result = getResult();

            Assert.AreEqual(expected, result);

            // List
            node = new TagNodeList(TagType.TAG_SHORT);
            ((TagNodeList)node).Add(new TagNodeShort(3));
            expected = "<TAG_LIST type=\"TAG_SHORT\"><TAG_SHORT>3</TAG_SHORT></TAG_LIST>";

            target.SerializeStart(writer, node);
            result = getResult();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagEndTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute
            node = new TagNodeNull();
            name = null;
            expected = "<TAG_END />";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Attribute
            node = new TagNodeNull();
            name = "name1";
            expected = "<TAG_END name=\"name1\" />";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagByteTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeByte();
            name = null;
            expected = "<TAG_BYTE>0</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeByte(27);
            name = null;
            expected = "<TAG_BYTE>27</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeByte(27);
            name = "name1";
            expected = "<TAG_BYTE name=\"name1\">27</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Numerical Tests
            node = new TagNodeByte(255);
            name = "name1";
            expected = "<TAG_BYTE name=\"name1\">255</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Hex Tests
            target.ByteAsHex = true;
            node = new TagNodeByte(0);
            name = "name1";
            expected = "<TAG_BYTE name=\"name1\">00</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeByte(2);
            name = "name1";
            expected = "<TAG_BYTE name=\"name1\">02</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeByte(27);
            name = "name1";
            expected = "<TAG_BYTE name=\"name1\">1B</TAG_BYTE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagShortTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeShort();
            name = null;
            expected = "<TAG_SHORT>0</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeShort(27);
            name = null;
            expected = "<TAG_SHORT>27</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeShort(27);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">27</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Numerical Tests
            node = new TagNodeShort(short.MaxValue);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">32767</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeShort(short.MinValue);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">-32768</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Hex Tests
            target.ShortAsHex = true;
            node = new TagNodeShort(0);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">0000</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeShort(2);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">0002</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeShort(27);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">001B</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeShort(-258);
            name = "name1";
            expected = "<TAG_SHORT name=\"name1\">FEFE</TAG_SHORT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagIntTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeInt();
            name = null;
            expected = "<TAG_INT>0</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeInt(27);
            name = null;
            expected = "<TAG_INT>27</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeInt(27);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">27</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Numerical Tests
            node = new TagNodeInt(int.MaxValue);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">2147483647</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeInt(int.MinValue);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">-2147483648</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Hex Tests
            target.IntAsHex = true;
            node = new TagNodeInt(0);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">00000000</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeInt(2);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">00000002</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeInt(27);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">0000001B</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeInt(-258);
            name = "name1";
            expected = "<TAG_INT name=\"name1\">FFFFFEFE</TAG_INT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagLongTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeLong();
            name = null;
            expected = "<TAG_LONG>0</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeLong(27);
            name = null;
            expected = "<TAG_LONG>27</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeLong(27);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">27</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Numerical Tests
            node = new TagNodeLong(long.MaxValue);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">9223372036854775807</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeLong(long.MinValue);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">-9223372036854775808</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Hex Tests
            target.LongAsHex = true;
            node = new TagNodeLong(0);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">0000000000000000</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeLong(2);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">0000000000000002</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeLong(27);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">000000000000001B</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeLong(-258);
            name = "name1";
            expected = "<TAG_LONG name=\"name1\">FFFFFFFFFFFFFEFE</TAG_LONG>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagFloatTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeFloat();
            name = null;
            expected = "<TAG_FLOAT>0</TAG_FLOAT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeFloat(1.12f);
            name = null;
            expected = "<TAG_FLOAT>1.12</TAG_FLOAT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeFloat(1.12f);
            name = "name1";
            expected = "<TAG_FLOAT name=\"name1\">1.12</TAG_FLOAT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Numerical Tests
            node = new TagNodeFloat(-1.25f);
            name = "name1";
            expected = "<TAG_FLOAT name=\"name1\">-1.25</TAG_FLOAT>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagDoubleTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeDouble();
            name = null;
            expected = "<TAG_DOUBLE>0</TAG_DOUBLE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeDouble(1.12);
            name = null;
            expected = "<TAG_DOUBLE>1.12</TAG_DOUBLE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeDouble(1.12);
            name = "name1";
            expected = "<TAG_DOUBLE name=\"name1\">1.12</TAG_DOUBLE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Numerical Tests
            node = new TagNodeDouble(-1.25);
            name = "name1";
            expected = "<TAG_DOUBLE name=\"name1\">-1.25</TAG_DOUBLE>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagByteArrayTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();
            target.ArraySep = ' ';
            target.ByteArrayAsHex = false;

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeByteArray();
            name = null;
            expected = "<TAG_BYTE_ARRAY />";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeByteArray(new byte[] { 0 });
            name = null;
            expected = "<TAG_BYTE_ARRAY>0</TAG_BYTE_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            node = new TagNodeByteArray(new byte[] { 0, 1, 2, 3 });
            name = null;
            expected = "<TAG_BYTE_ARRAY>0 1 2 3</TAG_BYTE_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeByteArray(new byte[] { 4, 5 } );
            name = "name1";
            expected = "<TAG_BYTE_ARRAY name=\"name1\">4 5</TAG_BYTE_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Hex Tests
            target.ByteArrayAsHex = true;
            node = new TagNodeByteArray();
            name = null;
            expected = "<TAG_BYTE_ARRAY />";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeByteArray(new byte[] { 0, 4, 27 });
            name = "name1";
            expected = "<TAG_BYTE_ARRAY name=\"name1\">00 04 1B</TAG_BYTE_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagStringTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();
            target.ArraySep = ' ';
            target.IntArrayAsHex = false;

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeString();
            name = null;
            expected = "<TAG_STRING></TAG_STRING>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeString();
            ((TagNodeString)node).Data = null;
            name = null;
            expected = "<TAG_STRING />";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeString("");
            name = null;
            expected = "<TAG_STRING></TAG_STRING>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            node = new TagNodeString("efg");
            name = null;
            expected = "<TAG_STRING>efg</TAG_STRING>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeString("hij");
            name = "name1";
            expected = "<TAG_STRING name=\"name1\">hij</TAG_STRING>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagListTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNodeList node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeList(TagType.TAG_BYTE);
            name = null;
            expected = "<TAG_LIST type=\"TAG_BYTE\" />";

            target.SerializeList(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeList(TagType.TAG_BYTE, new List<TagNode>());
            name = null;
            expected = "<TAG_LIST type=\"TAG_BYTE\" />";

            target.SerializeList(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeList(TagType.TAG_BYTE);
            node.Add(new TagNodeByte(4));
            node.Add(new TagNodeByte(5));
            name = null;
            expected = "<TAG_LIST type=\"TAG_BYTE\">"
                      + "<TAG_BYTE>4</TAG_BYTE>"
                      + "<TAG_BYTE>5</TAG_BYTE>"
                     + "</TAG_LIST>";

            target.SerializeList(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeList(TagType.TAG_BYTE);
            node.Add(new TagNodeByte(4));
            name = "name1";
            expected = "<TAG_LIST name=\"name1\" type=\"TAG_BYTE\">"
                      + "<TAG_BYTE>4</TAG_BYTE>"
                     + "</TAG_LIST>";

            target.SerializeList(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // List with List inside
            node = new TagNodeList(TagType.TAG_LIST);
            TagNodeList tnl2 = new TagNodeList(TagType.TAG_BYTE);
            tnl2.Add(new TagNodeByte(7));
            node.Add(tnl2);
            name = null;
            expected = "<TAG_LIST type=\"TAG_LIST\">"
                    + "<TAG_LIST type=\"TAG_BYTE\">"
                      + "<TAG_BYTE>7</TAG_BYTE>"
                      + "</TAG_LIST>"
                     + "</TAG_LIST>";

            target.SerializeList(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // List with Compound inside
            node = new TagNodeList(TagType.TAG_COMPOUND);
            TagNodeCompound tnc = new TagNodeCompound();
            tnc.Add("name2", new TagNodeByte(8));
            node.Add(tnc);
            name = null;
            expected = "<TAG_LIST type=\"TAG_COMPOUND\">"
                    + "<TAG_COMPOUND>"
                      + "<TAG_BYTE name=\"name2\">8</TAG_BYTE>"
                      + "</TAG_COMPOUND>"
                     + "</TAG_LIST>";

            target.SerializeList(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagCompoundTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();

            TagNodeCompound node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeCompound();
            name = null;
            expected = "<TAG_COMPOUND />";

            target.SerializeCompound(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeCompound();
            node.Add("name2", new TagNodeByte(1));
            name = null;
            expected = "<TAG_COMPOUND><TAG_BYTE name=\"name2\">1</TAG_BYTE></TAG_COMPOUND>";

            target.SerializeCompound(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeCompound();
            node.Add("name2", new TagNodeByte(1));
            node.Add("name3", new TagNodeByte(2));
            name = "name1";
            expected = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_BYTE name=\"name2\">1</TAG_BYTE>"
                + "<TAG_BYTE name=\"name3\">2</TAG_BYTE></TAG_COMPOUND>";

            target.SerializeCompound(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Compound with List inside
            node = new TagNodeCompound();
            TagNodeList tnl = new TagNodeList(TagType.TAG_BYTE);
            tnl.Add(new TagNodeByte(2));
            node.Add("name2", tnl);
            node.Add("name3", new TagNodeByte(3));
            name = "name1";
            expected = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_LIST name=\"name2\" type=\"TAG_BYTE\"><TAG_BYTE>2</TAG_BYTE></TAG_LIST>"
                + "<TAG_BYTE name=\"name3\">3</TAG_BYTE></TAG_COMPOUND>";

            target.SerializeCompound(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Compound with Compound inside
            node = new TagNodeCompound();
            TagNodeCompound tnc2 = new TagNodeCompound();
            tnc2.Add("name4", new TagNodeByte(4));
            tnc2.Add("name5", new TagNodeInt(5));
            node.Add("name2", tnc2);
            node.Add("name3", new TagNodeFloat(3.2f));
            name = "name1";
            expected = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_COMPOUND name=\"name2\">"
                + "<TAG_BYTE name=\"name4\">4</TAG_BYTE>"
                + "<TAG_INT name=\"name5\">5</TAG_INT>"
                + "</TAG_COMPOUND>"
                + "<TAG_FLOAT name=\"name3\">3.2</TAG_FLOAT>"
                + "</TAG_COMPOUND>";

            target.SerializeCompound(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void SerializeTagIntArrayTest() {
            XmlSerializer_Accessor target = new XmlSerializer_Accessor();
            target.ArraySep = ' ';
            target.IntArrayAsHex = false;

            TagNode node;
            string name;
            string result;
            string expected;

            // No Attribute, No Value
            node = new TagNodeIntArray();
            name = null;
            expected = "<TAG_INT_ARRAY />";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // No Attribute, Yes Value
            node = new TagNodeIntArray(new int[] { 0 });
            name = null;
            expected = "<TAG_INT_ARRAY>0</TAG_INT_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            node = new TagNodeIntArray(new int[] { 0, 1, 2, 3 });
            name = null;
            expected = "<TAG_INT_ARRAY>0 1 2 3</TAG_INT_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();

            Assert.AreEqual(expected, result);

            // Yes Attribute, Yes Value
            node = new TagNodeIntArray(new int[] { 4, 5 });
            name = "name1";
            expected = "<TAG_INT_ARRAY name=\"name1\">4 5</TAG_INT_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            // Hex Tests
            target.IntArrayAsHex = true;
            node = new TagNodeIntArray();
            name = null;
            expected = "<TAG_INT_ARRAY />";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);

            node = new TagNodeIntArray(new int[] { 0, 4, 27 });
            name = "name1";
            expected = "<TAG_INT_ARRAY name=\"name1\">00000000 00000004 0000001B</TAG_INT_ARRAY>";

            target.SerializeScalar(writer, node, name);
            result = getResult();
            Assert.AreEqual(expected, result);
        }
    }
}
