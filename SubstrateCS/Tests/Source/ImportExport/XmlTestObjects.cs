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
    public class XmlTestScenario {
        private string _destXml;
        private string _sourceXml;
        private string _name;
        private TagNode _node;

        public XmlTestScenario(TagNode node, string name, string xml) 
                : this(node, name, xml, xml) {
        }

        public XmlTestScenario(TagNode node, string name, string destXml, string sourceXml) {
            this._node = node;
            this._name = name;
            this._destXml = destXml;
            this._sourceXml = sourceXml;
        }

        public string DestXml { get { return _destXml; } }
        public string SourceXml { get { return _sourceXml; } }
        public string Name { get { return _name; } }
        public TagNode Node { get { return _node; } }

        // D - deserialize test only
        // S - serialze test only
        // H - hex test

        public static readonly XmlTestScenario TAG_END_1;
        public static readonly XmlTestScenario TAG_END_1_D;
        public static readonly XmlTestScenario TAG_END_2;
        public static readonly XmlTestScenario TAG_END_2_D;

        public static readonly XmlTestScenario TAG_BYTE_1;
        public static readonly XmlTestScenario TAG_BYTE_1_D;
        public static readonly XmlTestScenario TAG_BYTE_2;
        public static readonly XmlTestScenario TAG_BYTE_3;
        public static readonly XmlTestScenario TAG_BYTE_4;
        public static readonly XmlTestScenario TAG_BYTE_5_H;
        public static readonly XmlTestScenario TAG_BYTE_6_H;
        public static readonly XmlTestScenario TAG_BYTE_7_H;

        public static readonly XmlTestScenario TAG_SHORT_1;
        public static readonly XmlTestScenario TAG_SHORT_1_D;
        public static readonly XmlTestScenario TAG_SHORT_2;
        public static readonly XmlTestScenario TAG_SHORT_3;
        public static readonly XmlTestScenario TAG_SHORT_4;
        public static readonly XmlTestScenario TAG_SHORT_5;
        public static readonly XmlTestScenario TAG_SHORT_6_H;
        public static readonly XmlTestScenario TAG_SHORT_7_H;
        public static readonly XmlTestScenario TAG_SHORT_8_H;

        public static readonly XmlTestScenario TAG_INT_1;
        public static readonly XmlTestScenario TAG_INT_1_D;
        public static readonly XmlTestScenario TAG_INT_2;
        public static readonly XmlTestScenario TAG_INT_3;
        public static readonly XmlTestScenario TAG_INT_4;
        public static readonly XmlTestScenario TAG_INT_5;
        public static readonly XmlTestScenario TAG_INT_6_H;
        public static readonly XmlTestScenario TAG_INT_7_H;
        public static readonly XmlTestScenario TAG_INT_8_H;
        public static readonly XmlTestScenario TAG_INT_9_H;

        public static readonly XmlTestScenario TAG_LONG_1;
        public static readonly XmlTestScenario TAG_LONG_1_D;
        public static readonly XmlTestScenario TAG_LONG_2;
        public static readonly XmlTestScenario TAG_LONG_3;
        public static readonly XmlTestScenario TAG_LONG_4;
        public static readonly XmlTestScenario TAG_LONG_5;
        public static readonly XmlTestScenario TAG_LONG_6_H;
        public static readonly XmlTestScenario TAG_LONG_7_H;
        public static readonly XmlTestScenario TAG_LONG_8_H;
        public static readonly XmlTestScenario TAG_LONG_9_H;

        public static readonly XmlTestScenario TAG_FLOAT_1;
        public static readonly XmlTestScenario TAG_FLOAT_1_D;
        public static readonly XmlTestScenario TAG_FLOAT_2;
        public static readonly XmlTestScenario TAG_FLOAT_3;
        public static readonly XmlTestScenario TAG_FLOAT_4;

        public static readonly XmlTestScenario TAG_DOUBLE_1;
        public static readonly XmlTestScenario TAG_DOUBLE_1_D;
        public static readonly XmlTestScenario TAG_DOUBLE_2;
        public static readonly XmlTestScenario TAG_DOUBLE_3;
        public static readonly XmlTestScenario TAG_DOUBLE_4;

        public static readonly XmlTestScenario TAG_BYTE_ARRAY_1;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_1_D;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_2;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_3;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_4;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_5_H;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_5_HD;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_6_H;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_7;
        public static readonly XmlTestScenario TAG_BYTE_ARRAY_7_D;

        public static readonly XmlTestScenario TAG_STRING_1;
        public static readonly XmlTestScenario TAG_STRING_2;
        public static readonly XmlTestScenario TAG_STRING_2_D;
        public static readonly XmlTestScenario TAG_STRING_3;
        public static readonly XmlTestScenario TAG_STRING_4;
        public static readonly XmlTestScenario TAG_STRING_5;

        public static readonly XmlTestScenario TAG_LIST_1;
        public static readonly XmlTestScenario TAG_LIST_1_D;
        public static readonly XmlTestScenario TAG_LIST_2;
        public static readonly XmlTestScenario TAG_LIST_2_D;
        public static readonly XmlTestScenario TAG_LIST_3;
        public static readonly XmlTestScenario TAG_LIST_4;
        public static readonly XmlTestScenario TAG_LIST_5;
        public static readonly XmlTestScenario TAG_LIST_5_D;
        public static readonly XmlTestScenario TAG_LIST_6;
        public static readonly XmlTestScenario TAG_LIST_7;
        public static readonly XmlTestScenario TAG_LIST_7_D;
        public static readonly XmlTestScenario TAG_LIST_8;
        public static readonly XmlTestScenario TAG_LIST_9;
        public static readonly XmlTestScenario TAG_LIST_9_D;

        public static readonly XmlTestScenario TAG_COMPOUND_1;
        public static readonly XmlTestScenario TAG_COMPOUND_1_D;
        public static readonly XmlTestScenario TAG_COMPOUND_2;
        public static readonly XmlTestScenario TAG_COMPOUND_3;
        public static readonly XmlTestScenario TAG_COMPOUND_4;
        public static readonly XmlTestScenario TAG_COMPOUND_4_D;
        public static readonly XmlTestScenario TAG_COMPOUND_5;
        public static readonly XmlTestScenario TAG_COMPOUND_6;
        public static readonly XmlTestScenario TAG_COMPOUND_6_D;
        public static readonly XmlTestScenario TAG_COMPOUND_7;
        public static readonly XmlTestScenario TAG_COMPOUND_8;
        public static readonly XmlTestScenario TAG_COMPOUND_8_D;

        public static readonly XmlTestScenario TAG_INT_ARRAY_1;
        public static readonly XmlTestScenario TAG_INT_ARRAY_1_D;
        public static readonly XmlTestScenario TAG_INT_ARRAY_2;
        public static readonly XmlTestScenario TAG_INT_ARRAY_3;
        public static readonly XmlTestScenario TAG_INT_ARRAY_4;
        public static readonly XmlTestScenario TAG_INT_ARRAY_5_H;
        public static readonly XmlTestScenario TAG_INT_ARRAY_5_HD;
        public static readonly XmlTestScenario TAG_INT_ARRAY_6_H;

        static XmlTestScenario() {
            TagNode node;
            string name;
            string xml;
            string xml2;

            // ### TAG_END ###

            node = new TagNodeNull();
            name = null;
            xml = "<TAG_END></TAG_END>";

            TAG_END_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeNull();
            name = null;
            xml = "<TAG_END />";

            TAG_END_1_D = new XmlTestScenario(node, name, xml);

            node = new TagNodeNull();
            name = "name1";
            xml = "<TAG_END name=\"name1\"></TAG_END>";

            TAG_END_2 = new XmlTestScenario(node, name, xml);

            node = new TagNodeNull();
            name = "name1";
            xml = "<TAG_END name=\"name1\" />";

            TAG_END_2_D = new XmlTestScenario(node, name, xml);

            // ### TAG_BYTE ###

            node = new TagNodeByte();
            name = null;
            xml = "<TAG_BYTE>0</TAG_BYTE>";

            TAG_BYTE_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeByte();
            name = null;
            xml = "<TAG_BYTE />";

            TAG_BYTE_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeByte(27);
            name = null;
            xml = "<TAG_BYTE>27</TAG_BYTE>";

            TAG_BYTE_2 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeByte(27);
            name = "name1";
            xml = "<TAG_BYTE name=\"name1\">27</TAG_BYTE>";

            TAG_BYTE_3 = new XmlTestScenario(node, name, xml);

            // Numerical Tests
            node = new TagNodeByte(255);
            name = "name1";
            xml = "<TAG_BYTE name=\"name1\">255</TAG_BYTE>";

            TAG_BYTE_4 = new XmlTestScenario(node, name, xml);

            // Hex Tests
            node = new TagNodeByte(0);
            name = "name1";
            xml = "<TAG_BYTE name=\"name1\">00</TAG_BYTE>";

            TAG_BYTE_5_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeByte(2);
            name = "name1";
            xml = "<TAG_BYTE name=\"name1\">02</TAG_BYTE>";

            TAG_BYTE_6_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeByte(27);
            name = "name1";
            xml = "<TAG_BYTE name=\"name1\">1B</TAG_BYTE>";

            TAG_BYTE_7_H = new XmlTestScenario(node, name, xml);

            // ### TAG_SHORT ###

            // No Attribute, No Value
            node = new TagNodeShort();
            name = null;
            xml = "<TAG_SHORT>0</TAG_SHORT>";

            TAG_SHORT_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeShort();
            name = null;
            xml = "<TAG_SHORT />";

            TAG_SHORT_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeShort(27);
            name = null;
            xml = "<TAG_SHORT>27</TAG_SHORT>";

            TAG_SHORT_2 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeShort(27);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">27</TAG_SHORT>";

            TAG_SHORT_3 = new XmlTestScenario(node, name, xml);

            // Numerical Tests
            node = new TagNodeShort(short.MaxValue);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">32767</TAG_SHORT>";

            TAG_SHORT_4 = new XmlTestScenario(node, name, xml);

            node = new TagNodeShort(short.MinValue);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">-32768</TAG_SHORT>";

            TAG_SHORT_5 = new XmlTestScenario(node, name, xml);

            // Hex Tests
            node = new TagNodeShort(0);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">0000</TAG_SHORT>";

            TAG_SHORT_6_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeShort(2);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">0002</TAG_SHORT>";

            TAG_SHORT_7_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeShort(27);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">001B</TAG_SHORT>";

            TAG_SHORT_8_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeShort(-258);
            name = "name1";
            xml = "<TAG_SHORT name=\"name1\">FEFE</TAG_SHORT>";

            // ### TAG_INT ###

            // No Attribute, No Value
            node = new TagNodeInt();
            name = null;
            xml = "<TAG_INT>0</TAG_INT>";

            TAG_INT_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeInt();
            name = null;
            xml = "<TAG_INT />";

            TAG_INT_1_D = new XmlTestScenario(node, name, xml);
            
            // No Attribute, Yes Value
            node = new TagNodeInt(27);
            name = null;
            xml = "<TAG_INT>27</TAG_INT>";

            TAG_INT_2 = new XmlTestScenario(node, name, xml);
            
            // Yes Attribute, Yes Value
            node = new TagNodeInt(27);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">27</TAG_INT>";

            TAG_INT_3 = new XmlTestScenario(node, name, xml);

            // Numerical Tests
            node = new TagNodeInt(int.MaxValue);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">2147483647</TAG_INT>";

            TAG_INT_4 = new XmlTestScenario(node, name, xml);

            node = new TagNodeInt(int.MinValue);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">-2147483648</TAG_INT>";

            TAG_INT_5 = new XmlTestScenario(node, name, xml);

            // Hex Tests
            node = new TagNodeInt(0);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">00000000</TAG_INT>";

            TAG_INT_6_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeInt(2);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">00000002</TAG_INT>";

            TAG_INT_7_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeInt(27);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">0000001B</TAG_INT>";

            TAG_INT_8_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeInt(-258);
            name = "name1";
            xml = "<TAG_INT name=\"name1\">FFFFFEFE</TAG_INT>";

            TAG_INT_9_H = new XmlTestScenario(node, name, xml);

            // ### TAG_LONG ###

            // No Attribute, No Value
            node = new TagNodeLong();
            name = null;
            xml = "<TAG_LONG>0</TAG_LONG>";

            TAG_LONG_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeLong();
            name = null;
            xml = "<TAG_LONG />";

            TAG_LONG_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeLong(27);
            name = null;
            xml = "<TAG_LONG>27</TAG_LONG>";

            TAG_LONG_2 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeLong(27);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">27</TAG_LONG>";

            TAG_LONG_3 = new XmlTestScenario(node, name, xml);
            
            // Numerical Tests
            node = new TagNodeLong(long.MaxValue);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">9223372036854775807</TAG_LONG>";

            TAG_LONG_4 = new XmlTestScenario(node, name, xml);

            node = new TagNodeLong(long.MinValue);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">-9223372036854775808</TAG_LONG>";

            TAG_LONG_5 = new XmlTestScenario(node, name, xml);
            
            // Hex Tests
            node = new TagNodeLong(0);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">0000000000000000</TAG_LONG>";

            TAG_LONG_6_H = new XmlTestScenario(node, name, xml);
            
            node = new TagNodeLong(2);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">0000000000000002</TAG_LONG>";

            TAG_LONG_7_H = new XmlTestScenario(node, name, xml);
            
            node = new TagNodeLong(27);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">000000000000001B</TAG_LONG>";

            TAG_LONG_8_H = new XmlTestScenario(node, name, xml);
            
            node = new TagNodeLong(-258);
            name = "name1";
            xml = "<TAG_LONG name=\"name1\">FFFFFFFFFFFFFEFE</TAG_LONG>";

            TAG_LONG_9_H = new XmlTestScenario(node, name, xml);
            
            // ### TAG_FLOAT ###
            // No Attribute, No Value
            node = new TagNodeFloat();
            name = null;
            xml = "<TAG_FLOAT>0</TAG_FLOAT>";

            TAG_FLOAT_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeFloat();
            name = null;
            xml = "<TAG_FLOAT />";

            TAG_FLOAT_1_D = new XmlTestScenario(node, name, xml);
            
            // No Attribute, Yes Value
            node = new TagNodeFloat(1.12f);
            name = null;
            xml = "<TAG_FLOAT>1.12</TAG_FLOAT>";

            TAG_FLOAT_2 = new XmlTestScenario(node, name, xml);
            
            // Yes Attribute, Yes Value
            node = new TagNodeFloat(1.12f);
            name = "name1";
            xml = "<TAG_FLOAT name=\"name1\">1.12</TAG_FLOAT>";

            TAG_FLOAT_3 = new XmlTestScenario(node, name, xml);

            // Numerical Tests
            node = new TagNodeFloat(-1.25f);
            name = "name1";
            xml = "<TAG_FLOAT name=\"name1\">-1.25</TAG_FLOAT>";

            TAG_FLOAT_4 = new XmlTestScenario(node, name, xml);

            // ### TAG_DOUBLE ###
            node = new TagNodeDouble();
            name = null;
            xml = "<TAG_DOUBLE>0</TAG_DOUBLE>";

            TAG_DOUBLE_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeDouble();
            name = null;
            xml = "<TAG_DOUBLE />";

            TAG_DOUBLE_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeDouble(1.12);
            name = null;
            xml = "<TAG_DOUBLE>1.12</TAG_DOUBLE>";

            TAG_DOUBLE_2 = new XmlTestScenario(node, name, xml);
            
            // Yes Attribute, Yes Value
            node = new TagNodeDouble(1.12);
            name = "name1";
            xml = "<TAG_DOUBLE name=\"name1\">1.12</TAG_DOUBLE>";

            TAG_DOUBLE_3 = new XmlTestScenario(node, name, xml);
            
            // Numerical Tests
            node = new TagNodeDouble(-1.25);
            name = "name1";
            xml = "<TAG_DOUBLE name=\"name1\">-1.25</TAG_DOUBLE>";

            TAG_DOUBLE_4 = new XmlTestScenario(node, name, xml);

            // ### TAG_BYTE_ARRAY

            // No Attribute, No Value
            node = new TagNodeByteArray();
            name = null;
            xml = "<TAG_BYTE_ARRAY></TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeByteArray();
            name = null;
            xml = "<TAG_BYTE_ARRAY />";

            TAG_BYTE_ARRAY_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeByteArray(new byte[] { 0 });
            name = null;
            xml = "<TAG_BYTE_ARRAY>0</TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_2 = new XmlTestScenario(node, name, xml);

            node = new TagNodeByteArray(new byte[] { 0, 1, 2, 3 });
            name = null;
            xml = "<TAG_BYTE_ARRAY>0 1 2 3</TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_3 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeByteArray(new byte[] { 4, 5 });
            name = "name1";
            xml = "<TAG_BYTE_ARRAY name=\"name1\">4 5</TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_4 = new XmlTestScenario(node, name, xml);

            // Hex Tests
            node = new TagNodeByteArray();
            name = null;
            xml = "<TAG_BYTE_ARRAY></TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_5_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeByteArray();
            name = null;
            xml = "<TAG_BYTE_ARRAY />";

            TAG_BYTE_ARRAY_5_HD = new XmlTestScenario(node, name, xml);

            node = new TagNodeByteArray(new byte[] { 0, 4, 27 });
            name = "name1";
            xml = "<TAG_BYTE_ARRAY name=\"name1\">00 04 1B</TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_6_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeByteArray(null);
            name = "name1";
            xml = "<TAG_BYTE_ARRAY name=\"name1\"></TAG_BYTE_ARRAY>";

            TAG_BYTE_ARRAY_7 = new XmlTestScenario(node, name, xml);

            node = new TagNodeByteArray(null);
            name = "name1";
            xml = "<TAG_BYTE_ARRAY name=\"name1\" />";

            TAG_BYTE_ARRAY_7_D = new XmlTestScenario(node, name, xml);

            // ### TAG_STRING
            // No Attribute, No Value
            node = new TagNodeString();
            name = null;
            xml = "<TAG_STRING></TAG_STRING>";

            TAG_STRING_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeString();
            ((TagNodeString)node).Data = null;
            name = null;
            xml = "<TAG_STRING></TAG_STRING>";

            TAG_STRING_2 = new XmlTestScenario(node, name, xml);

            node = new TagNodeString();
            ((TagNodeString)node).Data = null;
            name = null;
            xml = "<TAG_STRING />";

            TAG_STRING_2_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeString("");
            name = null;
            xml = "<TAG_STRING></TAG_STRING>";

            TAG_STRING_3 = new XmlTestScenario(node, name, xml);

            node = new TagNodeString("efg");
            name = null;
            xml = "<TAG_STRING>efg</TAG_STRING>";

            TAG_STRING_4 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeString("hij");
            name = "name1";
            xml = "<TAG_STRING name=\"name1\">hij</TAG_STRING>";

            TAG_STRING_5 = new XmlTestScenario(node, name, xml);

            // ### TAG_LIST

            // No Attribute, No Value
            node = new TagNodeList(TagType.TAG_BYTE);
            name = null;
            xml = "<TAG_LIST type=\"TAG_BYTE\"></TAG_LIST>";

            TAG_LIST_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_BYTE);
            name = null;
            xml = "<TAG_LIST type=\"TAG_BYTE\" />";

            TAG_LIST_1_D = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_BYTE, new List<TagNode>());
            name = null;
            xml = "<TAG_LIST type=\"TAG_BYTE\"></TAG_LIST>";

            TAG_LIST_2 = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_BYTE, new List<TagNode>());
            name = null;
            xml = "<TAG_LIST type=\"TAG_BYTE\" />";

            TAG_LIST_2_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeList(TagType.TAG_BYTE);
            ((TagNodeList)node).Add(new TagNodeByte(4));
            ((TagNodeList)node).Add(new TagNodeByte(5));
            name = null;
            xml = "<TAG_LIST type=\"TAG_BYTE\">"
                      + "<TAG_BYTE>4</TAG_BYTE>"
                      + "<TAG_BYTE>5</TAG_BYTE>"
                     + "</TAG_LIST>";

            TAG_LIST_3 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeList(TagType.TAG_BYTE);
            ((TagNodeList)node).Add(new TagNodeByte(4));
            name = "name1";
            xml = "<TAG_LIST name=\"name1\" type=\"TAG_BYTE\">"
                      + "<TAG_BYTE>4</TAG_BYTE>"
                     + "</TAG_LIST>";

            TAG_LIST_4 = new XmlTestScenario(node, name, xml);

            // List with List inside
            node = new TagNodeList(TagType.TAG_LIST);
            TagNodeList tnl2 = new TagNodeList(TagType.TAG_BYTE);
            ((TagNodeList)node).Add(tnl2);
            name = null;
            xml = "<TAG_LIST type=\"TAG_LIST\">"
                    + "<TAG_LIST type=\"TAG_BYTE\"></TAG_LIST>"
                     + "</TAG_LIST>";

            TAG_LIST_5 = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_LIST);
            tnl2 = new TagNodeList(TagType.TAG_BYTE);
            ((TagNodeList)node).Add(tnl2);
            name = null;
            xml = "<TAG_LIST type=\"TAG_LIST\">"
                    + "<TAG_LIST type=\"TAG_BYTE\" />"
                     + "</TAG_LIST>";

            TAG_LIST_5_D = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_LIST);
            tnl2 = new TagNodeList(TagType.TAG_BYTE);
            tnl2.Add(new TagNodeByte(7));
            ((TagNodeList)node).Add(tnl2);
            name = null;
            xml = "<TAG_LIST type=\"TAG_LIST\">"
                    + "<TAG_LIST type=\"TAG_BYTE\">"
                      + "<TAG_BYTE>7</TAG_BYTE>"
                      + "</TAG_LIST>"
                     + "</TAG_LIST>";

            TAG_LIST_6 = new XmlTestScenario(node, name, xml);

            // List with Compound inside
            node = new TagNodeList(TagType.TAG_COMPOUND);
            TagNodeCompound tnc = new TagNodeCompound();
            ((TagNodeList)node).Add(tnc);
            name = null;
            xml = "<TAG_LIST type=\"TAG_COMPOUND\">"
                    + "<TAG_COMPOUND></TAG_COMPOUND>"
                     + "</TAG_LIST>";

            TAG_LIST_7 = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_COMPOUND);
            tnc = new TagNodeCompound();
            ((TagNodeList)node).Add(tnc);
            name = null;
            xml = "<TAG_LIST type=\"TAG_COMPOUND\">"
                    + "<TAG_COMPOUND />"
                     + "</TAG_LIST>";

            TAG_LIST_7_D = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_COMPOUND);
            tnc = new TagNodeCompound();
            tnc.Add("name2", new TagNodeByte(8));
            ((TagNodeList)node).Add(tnc);
            name = null;
            xml = "<TAG_LIST type=\"TAG_COMPOUND\">"
                    + "<TAG_COMPOUND>"
                      + "<TAG_BYTE name=\"name2\">8</TAG_BYTE>"
                      + "</TAG_COMPOUND>"
                     + "</TAG_LIST>";

            TAG_LIST_8 = new XmlTestScenario(node, name, xml);

            // List with empty scalar inside
            node = new TagNodeList(TagType.TAG_BYTE_ARRAY);
            ((TagNodeList)node).Add(new TagNodeByteArray(null));
            name = "name1";
            xml = "<TAG_LIST name=\"name1\" type=\"TAG_BYTE_ARRAY\">"
                      + "<TAG_BYTE_ARRAY></TAG_BYTE_ARRAY>"
                     + "</TAG_LIST>";

            TAG_LIST_9 = new XmlTestScenario(node, name, xml);

            node = new TagNodeList(TagType.TAG_BYTE_ARRAY);
            ((TagNodeList)node).Add(new TagNodeByteArray(null));
            name = "name1";
            xml = "<TAG_LIST name=\"name1\" type=\"TAG_BYTE_ARRAY\">"
                      + "<TAG_BYTE_ARRAY />"
                     + "</TAG_LIST>";

            TAG_LIST_9_D = new XmlTestScenario(node, name, xml);

            // ### TAG_COMPOUND

            // No Attribute, No Value
            node = new TagNodeCompound();
            name = null;
            xml = "<TAG_COMPOUND></TAG_COMPOUND>";

            TAG_COMPOUND_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeCompound();
            name = null;
            xml = "<TAG_COMPOUND />";

            TAG_COMPOUND_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", new TagNodeByte(1));
            name = null;
            xml = "<TAG_COMPOUND><TAG_BYTE name=\"name2\">1</TAG_BYTE></TAG_COMPOUND>";

            TAG_COMPOUND_2 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", new TagNodeByte(1));
            ((TagNodeCompound)node).Add("name3", new TagNodeByte(2));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_BYTE name=\"name2\">1</TAG_BYTE>"
                + "<TAG_BYTE name=\"name3\">2</TAG_BYTE></TAG_COMPOUND>";

            TAG_COMPOUND_3 = new XmlTestScenario(node, name, xml);

            // Compound with List inside
            node = new TagNodeCompound();
            TagNodeList tnl = new TagNodeList(TagType.TAG_BYTE);
            ((TagNodeCompound)node).Add("name2", tnl);
            ((TagNodeCompound)node).Add("name3", new TagNodeByte(3));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_LIST name=\"name2\" type=\"TAG_BYTE\"></TAG_LIST>"
                + "<TAG_BYTE name=\"name3\">3</TAG_BYTE></TAG_COMPOUND>";

            TAG_COMPOUND_4 = new XmlTestScenario(node, name, xml);

            node = new TagNodeCompound();
            tnl = new TagNodeList(TagType.TAG_BYTE);
            ((TagNodeCompound)node).Add("name2", tnl);
            ((TagNodeCompound)node).Add("name3", new TagNodeByte(3));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_LIST name=\"name2\" type=\"TAG_BYTE\" />"
                + "<TAG_BYTE name=\"name3\">3</TAG_BYTE></TAG_COMPOUND>";

            TAG_COMPOUND_4_D = new XmlTestScenario(node, name, xml);

            node = new TagNodeCompound();
            tnl = new TagNodeList(TagType.TAG_BYTE);
            tnl.Add(new TagNodeByte(2));
            ((TagNodeCompound)node).Add("name2", tnl);
            ((TagNodeCompound)node).Add("name3", new TagNodeByte(3));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_LIST name=\"name2\" type=\"TAG_BYTE\"><TAG_BYTE>2</TAG_BYTE></TAG_LIST>"
                + "<TAG_BYTE name=\"name3\">3</TAG_BYTE></TAG_COMPOUND>";

            TAG_COMPOUND_5 = new XmlTestScenario(node, name, xml);

            // Compound with Compound inside
            node = new TagNodeCompound();
            TagNodeCompound tnc2 = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", tnc2);
            ((TagNodeCompound)node).Add("name3", new TagNodeFloat(3.2f));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_COMPOUND name=\"name2\"></TAG_COMPOUND>"
                + "<TAG_FLOAT name=\"name3\">3.2</TAG_FLOAT>"
                + "</TAG_COMPOUND>";

            TAG_COMPOUND_6 = new XmlTestScenario(node, name, xml);

            node = new TagNodeCompound();
            tnc2 = new TagNodeCompound();
            ((TagNodeCompound)node).Add("name2", tnc2);
            ((TagNodeCompound)node).Add("name3", new TagNodeFloat(3.2f));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_COMPOUND name=\"name2\" />"
                + "<TAG_FLOAT name=\"name3\">3.2</TAG_FLOAT>"
                + "</TAG_COMPOUND>";

            TAG_COMPOUND_6_D = new XmlTestScenario(node, name, xml);

            node = new TagNodeCompound();
            tnc2 = new TagNodeCompound();
            tnc2.Add("name4", new TagNodeByte(4));
            tnc2.Add("name5", new TagNodeInt(5));
            ((TagNodeCompound)node).Add("name2", tnc2);
            ((TagNodeCompound)node).Add("name3", new TagNodeFloat(3.2f));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_COMPOUND name=\"name2\">"
                + "<TAG_BYTE name=\"name4\">4</TAG_BYTE>"
                + "<TAG_INT name=\"name5\">5</TAG_INT>"
                + "</TAG_COMPOUND>"
                + "<TAG_FLOAT name=\"name3\">3.2</TAG_FLOAT>"
                + "</TAG_COMPOUND>";

            TAG_COMPOUND_7 = new XmlTestScenario(node, name, xml);

            // Compound with empty scalar inside
            node = new TagNodeCompound();
            tnl = new TagNodeList(TagType.TAG_BYTE_ARRAY);
            ((TagNodeCompound)node).Add("name3", new TagNodeByteArray(null));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_BYTE_ARRAY name=\"name3\"></TAG_BYTE_ARRAY></TAG_COMPOUND>";

            TAG_COMPOUND_8 = new XmlTestScenario(node, name, xml);

            node = new TagNodeCompound();
            tnl = new TagNodeList(TagType.TAG_BYTE_ARRAY);
            ((TagNodeCompound)node).Add("name3", new TagNodeByteArray(null));
            name = "name1";
            xml = "<TAG_COMPOUND name=\"name1\">"
                + "<TAG_BYTE_ARRAY name=\"name3\" /></TAG_COMPOUND>";

            TAG_COMPOUND_8_D = new XmlTestScenario(node, name, xml);

            // ### TAG_INT_ARRAY

            // No Attribute, No Value
            node = new TagNodeIntArray();
            name = null;
            xml = "<TAG_INT_ARRAY></TAG_INT_ARRAY>";

            TAG_INT_ARRAY_1 = new XmlTestScenario(node, name, xml);

            node = new TagNodeIntArray();
            name = null;
            xml = "<TAG_INT_ARRAY />";

            TAG_INT_ARRAY_1_D = new XmlTestScenario(node, name, xml);

            // No Attribute, Yes Value
            node = new TagNodeIntArray(new int[] { 0 });
            name = null;
            xml = "<TAG_INT_ARRAY>0</TAG_INT_ARRAY>";

            TAG_INT_ARRAY_2 = new XmlTestScenario(node, name, xml);

            node = new TagNodeIntArray(new int[] { 0, 1, 2, 3 });
            name = null;
            xml = "<TAG_INT_ARRAY>0 1 2 3</TAG_INT_ARRAY>";

            TAG_INT_ARRAY_3 = new XmlTestScenario(node, name, xml);

            // Yes Attribute, Yes Value
            node = new TagNodeIntArray(new int[] { 4, 5 });
            name = "name1";
            xml = "<TAG_INT_ARRAY name=\"name1\">4 5</TAG_INT_ARRAY>";

            TAG_INT_ARRAY_4 = new XmlTestScenario(node, name, xml);

            // Hex Tests
            node = new TagNodeIntArray();
            name = null;
            xml = "<TAG_INT_ARRAY></TAG_INT_ARRAY>";

            TAG_INT_ARRAY_5_H = new XmlTestScenario(node, name, xml);

            node = new TagNodeIntArray();
            name = null;
            xml = "<TAG_INT_ARRAY />";

            TAG_INT_ARRAY_5_HD = new XmlTestScenario(node, name, xml);

            node = new TagNodeIntArray(new int[] { 0, 4, 27 });
            name = "name1";
            xml = "<TAG_INT_ARRAY name=\"name1\">00000000 00000004 0000001B</TAG_INT_ARRAY>";

            TAG_INT_ARRAY_6_H = new XmlTestScenario(node, name, xml);
        }
    }
}

