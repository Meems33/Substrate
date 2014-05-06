using Substrate.Nbt;
using Substrate.ImportExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeStartTest() {
            TagNode node;
            TagNode result;
            string xml;

            // Scalar
            node = new TagNodeByte(2);
            xml = "<TAG_BYTE>2</TAG_BYTE>";

            result = DeserializeStart(reader);
            AssertEqual(node, result);

            // Compound
            node = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", new TagNodeDouble(4.3));

            xml = "<TAG_COMPOUND><TAG_DOUBLE name=\"name2\">4.3</TAG_DOUBLE></TAG_COMPOUND>";

            DeserializeStart(reader);
            AssertEqual(node, result);

            // List
            node = new TagNodeList(TagType.TAG_SHORT);
            ((TagNodeList)node).Add(new TagNodeShort(3));
            xml = "<TAG_LIST type=\"TAG_SHORT\"><TAG_SHORT>3</TAG_SHORT></TAG_LIST>";

            DeserializeStart(reader);
            AssertEqual(node, result);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeTagEndTest() {
            // No Attribute
            doScalarTest(XmlTestScenario.TAG_END_1);

            // Attribute
            doScalarTest(XmlTestScenario.TAG_END_2);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeTagByteTest() {
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
        public void DeserializeTagShortTest() {
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
        public void DeserializeTagIntTest() {
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
        public void DeserializeTagLongTest() {
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
        public void DeserializeTagFloatTest() {
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
        public void DeserializeTagDoubleTest() {
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
        public void DeserializeTagByteArrayTest() {
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
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeTagStringTest() {
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
        public void DeserializeTagListTest() {
            // No Attribute, No Value
            doListTest(XmlTestScenario.TAG_LIST_1);
            doListTest(XmlTestScenario.TAG_LIST_2);

            // No Attribute, Yes Value
            doListTest(XmlTestScenario.TAG_LIST_3);

            // Yes Attribute, Yes Value
            doListTest(XmlTestScenario.TAG_LIST_4);

            // List with List inside
            doListTest(XmlTestScenario.TAG_LIST_5);

            // List with Compound inside
            doListTest(XmlTestScenario.TAG_LIST_6);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeTagCompoundTest() {
            // No Attribute, No Value
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_1);

            // No Attribute, Yes Value
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_2);

            // Yes Attribute, Yes Value
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_3);

            // Compound with List inside
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_4);

            // Compound with Compound inside
            doCompoundTest(XmlTestScenario.TAG_COMPOUND_5);
        }

        [TestMethod()]
        [DeploymentItem("Substrate.dll")]
        public void DeserializeTagIntArrayTest() {
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

        public void AssertEqual(TagNode expected, TagNode actual) {
            if (expected == null && actual == null) {
                // OK
            } else if (expected == null) {
                Assert.Fail();
            } else if (actual == null) {
                Assert.Fail();
            } else if (expected == actual) {
                // OK
            } else {
                if (expected.GetType() != actual.GetType()) {
                    Assert.Fail();
                }

                if (expected is TagNodeNull) {
                    TagNodeNull tn1 = expected as TagNodeNull;
                    TagNodeNull tn2 = actual as TagNodeNull;

                    // No properties... always equal
                } else if (expected is TagNodeByte) {
                    TagNodeByte tn1 = expected as TagNodeByte;
                    TagNodeByte tn2 = actual as TagNodeByte;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeShort) {
                    TagNodeShort tn1 = expected as TagNodeShort;
                    TagNodeShort tn2 = actual as TagNodeShort;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeInt) {
                    TagNodeInt tn1 = expected as TagNodeInt;
                    TagNodeInt tn2 = actual as TagNodeInt;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeLong) {
                    TagNodeLong tn1 = expected as TagNodeLong;
                    TagNodeLong tn2 = actual as TagNodeLong;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeFloat) {
                    TagNodeFloat tn1 = expected as TagNodeFloat;
                    TagNodeFloat tn2 = actual as TagNodeFloat;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeDouble) {
                    TagNodeDouble tn1 = expected as TagNodeDouble;
                    TagNodeDouble tn2 = actual as TagNodeDouble;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeByteArray) {
                    TagNodeByteArray tn1 = expected as TagNodeByteArray;
                    TagNodeByteArray tn2 = actual as TagNodeByteArray;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeIntArray) {
                    TagNodeIntArray tn1 = expected as TagNodeIntArray;
                    TagNodeIntArray tn2 = actual as TagNodeIntArray;

                    Assert.AreEqual(tn1.Data, tn2.Data);
                } else if (expected is TagNodeList) {
                    TagNodeList tn1 = expected as TagNodeList;
                    TagNodeList tn2 = actual as TagNodeList;

                    Assert.AreEqual(tn1.Count, tn2.Count);

                    IEnumerator<TagNode> it1 = tn1.GetEnumerator();
                    IEnumerator<TagNode> it2 = tn2.GetEnumerator();

                    while (it1.MoveNext()) {
                        it2.MoveNext();

                        AssertEqual(it1.Current, it2.Current);
                    }
                } else if (expected is TagNodeCompound) {
                    TagNodeCompound tn1 = expected as TagNodeCompound;
                    TagNodeCompound tn2 = actual as TagNodeCompound;

                    IEnumerator<KeyValuePair<string, TagNode>> it1 = tn1.GetEnumerator();
                    IEnumerator<KeyValuePair<string, TagNode>> it2 = tn2.GetEnumerator();

                    while (it1.MoveNext()) {
                        it2.MoveNext();

                        Assert.AreEqual(it1.Current.Key, it2.Current.Key);
                        AssertEqual(it1.Current.Value, it2.Current.Value);
                    }
                } else {
                    Assert.Fail();
                }
            }
        }

        private void doScalarTest(XmlTestScenario scenario) {
            TagNode result;
            string name;

            setInput(scenario.Xml, true);
            DeserializeScalar(reader, out result, out name);

            Assert.AreEqual(scenario.Name, name);
            AssertEqual(scenario.Node, result);
        }

        private void doListTest(XmlTestScenario scenario) {
            TagNodeList result;
            string name;

            setInput(scenario.Xml, true);
            DeserializeList(reader, out result, out name);

            Assert.AreEqual(scenario.Name, name);
            AssertEqual(scenario.Node, result);
        }

        private void doCompoundTest(XmlTestScenario scenario) {
            TagNodeCompound result;
            string name;

            setInput(scenario.Xml, true);
            DeserializeCompound(reader, out result, out name);

            Assert.AreEqual(scenario.Name, name);
            AssertEqual(scenario.Node, result);
        }

        private TagNode DeserializeStart(XmlReader reader) {
            object[] args = new object[] { reader };
            return (TagNode) wrapper.Invoke("DeserializeStart", args);
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
