
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

using Substrate.Nbt;

namespace Substrate.ImportExport {

    /// <summary>
    /// Deserializes XML into NBT tags.
    /// 
    /// Note the following:
    ///   - Null, empty, and XML elements with no content are deserialized using the 
    ///     NBT tag's default constructors.
    ///   - Unknown attributes are ignored
    ///   - In order to properly deserialize XML, the appropriate hex properties must
    ///     be set (see base class).
    /// 
    /// The XML format is described in NbtXml.xsd
    /// </summary>
    public class XmlDeserializer : XmlBase {

        /// <summary>
        /// Creates an XmlDeserializer
        /// </summary>
        public XmlDeserializer() {
        }

        /// <summary>
        /// Deserializes the given input stream into a TagNode using
        /// default XmlReader settings. The stream is closed upon completion.
        /// </summary>
        /// <param name="inputStream">The input stream</param>
        /// <returns>The resulting tag node</returns>
        public TagNode Deserialize(Stream inputStream) {
            if (inputStream == null) {
                throw new ArgumentNullException();
            }

            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.IgnoreComments = true;
            xmlSettings.IgnoreProcessingInstructions = true;

            XmlReader reader = XmlReader.Create(inputStream, xmlSettings);

            try {
                return Deserialize(reader);
            } finally {
                if (reader != null) {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Deserializes the XmlReader stream into a TagNode. The
        /// XmlReader is NOT closed upon completion.
        /// </summary>
        /// <param name="reader">The XmlReader</param>
        /// <returns>The deserialized TagNode</returns>
        public TagNode Deserialize(XmlReader reader) {
            if (reader == null) {
                throw new ArgumentNullException();
            }

            return DeserializeStart(reader);
        }

        protected TagNode DeserializeStart(XmlReader reader) {
            TagNode node = null;

            int numChildren = 0;
            while (!reader.EOF) {
                if (XmlNodeType.XmlDeclaration == reader.NodeType) {
                    reader.Read();
                } else if (XmlNodeType.Element == reader.NodeType) {
                    TagType childType = getTagType(reader.Name);

                    string outName;
                    
                    if (numChildren > 1) {
                        throw new InvalidTagException();
                    }

                    if (TagType.TAG_COMPOUND == childType) {
                        TagNodeCompound tnc;
                        DeserializeCompound(reader, out tnc, out outName);
                        node = tnc;
                    } else if (TagType.TAG_LIST == childType) {
                        TagNodeList tnl;
                        DeserializeList(reader, out tnl, out outName);
                        node = tnl;
                    } else {
                        DeserializeScalar(reader, out node, out outName);
                    }

                    ++numChildren;
                } else {
                    reader.Read();
                }
            }

            return node;
        }

        // We just read a TAG_COMPOUND
        protected void DeserializeCompound(XmlReader reader, out TagNodeCompound node, out string name) {
            //Console.WriteLine("DeserializeCompound");
            TagType type = getTagType(reader.Name);

            if (TagType.TAG_COMPOUND != type) {
                throw new InvalidTagException();
            }

            name = reader.GetAttribute("name");

            if (reader.IsEmptyElement) {
                node = new TagNodeCompound();

                // Move on to the next element
                reader.Read();
            } else {
                List<string> gatheredNames = new List<String>();
                List<TagNode> gatheredTags = new List<TagNode>();

                // Right now are are pointing at the element that starts TAG_COMPOUND
                // Need to move to the next element.
                if (!reader.EOF) {
                    reader.Read();
                }

                while (!reader.EOF) {
                    if (reader.IsStartElement()) {
                        // Child...
                        TagType childType = getTagType(reader.Name);

                        TagNode outTag;
                        string outName;

                        if (TagType.TAG_COMPOUND == childType) {
                            TagNodeCompound tnc;
                            DeserializeCompound(reader, out tnc, out outName);
                            outTag = tnc;
                        } else if (TagType.TAG_LIST == childType) {
                            TagNodeList tnl;
                            DeserializeList(reader, out tnl, out outName);
                            outTag = tnl;
                        } else {
                            DeserializeScalar(reader, out outTag, out outName);
                        }

                        gatheredTags.Add(outTag);
                        gatheredNames.Add(outName);
                    } else if (XmlNodeType.EndElement == reader.NodeType) {
                        // Children consume their own end elements so this must be ours
                        if (getTagType(reader.Name) != TagType.TAG_COMPOUND) {
                            throw new InvalidTagException();
                        }

                        reader.ReadEndElement();
                        break;
                    } else {
                        reader.Read();
                    }
                }

                node = new TagNodeCompound();

                for (int i = 0; i < gatheredTags.Count; ++i) {
                    node.Add(gatheredNames[i], gatheredTags[i]);
                }
            }
        }

        protected void DeserializeList(XmlReader reader, out TagNodeList node, out string name) {
            //Console.WriteLine("DeserializeList");
            TagType type = getTagType(reader.Name);

            if (TagType.TAG_LIST != type) {
                throw new InvalidTagException();
            }

            name = reader.GetAttribute("name");
            string innerType = reader.GetAttribute("type");
            TagType innerTypeTag = getTagType(innerType);

            if (reader.IsEmptyElement) {
                node = new TagNodeList(innerTypeTag);

                // Move on to the next element
                reader.Read();
            } else {
                List<TagNode> gatheredTags = new List<TagNode>();

                // Right now are are pointing at the element that starts TAG_LIST
                // Need to move to the next element.

                if (!reader.EOF) {
                    reader.Read();
                }

                while (!reader.EOF) {
                    if (reader.IsStartElement()) {
                        // Child...
                        TagType childType = getTagType(reader.Name);

                        TagNode outTag;
                        string outName;

                        if (TagType.TAG_COMPOUND == childType) {
                            TagNodeCompound tnc;
                            DeserializeCompound(reader, out tnc, out outName);
                            outTag = tnc;
                        } else if (TagType.TAG_LIST == childType) {
                            TagNodeList tnl;
                            DeserializeList(reader, out tnl, out outName);
                            outTag = tnl;
                        } else {
                            DeserializeScalar(reader, out outTag, out outName);
                        }

                        gatheredTags.Add(outTag);
                    } else if (XmlNodeType.EndElement == reader.NodeType) {
                        // Children consume their own end elements so this must be ours
                        if (getTagType(reader.Name) != TagType.TAG_LIST) {
                            throw new InvalidTagException();
                        }

                        reader.ReadEndElement();
                        break;
                    } else {
                        reader.Read();
                    }
                }

                if (gatheredTags == null) {
                    node = new TagNodeList(innerTypeTag);
                } else {
                    node = new TagNodeList(innerTypeTag, gatheredTags);
                }
            }
        }

        protected void DeserializeScalar(XmlReader reader, out TagNode node, out string name) {
            //Console.WriteLine("DeserializeScalar");
            TagType type = getTagType(reader.Name);

            // Let's read the attr if it exists
            name = reader.GetAttribute("name");
            string rawValue = readTextElement(reader);

            // By this point we have read everything that we need
            // to read and our reader is pointing to the next element.

            switch (type) {
                case TagType.TAG_END:
                    node = new TagNodeNull();
                    break;

                case TagType.TAG_BYTE:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeByte();
                    } else {
                        byte value;
                        if (ByteAsHex) {
                            value = ByteFromHex(rawValue);
                        } else {
                            value = byte.Parse(rawValue, NumberStyles.Integer);
                        }

                        node = new TagNodeByte(value);
                    }

                    break;

                case TagType.TAG_SHORT:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeShort();
                    } else {
                        short value;
                        if (ShortAsHex) {
                            value = ShortFromHex(rawValue);
                        } else {
                            value = short.Parse(rawValue, NumberStyles.Integer);
                        }

                        node = new TagNodeShort(value);
                    }

                    break;

                case TagType.TAG_INT:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeInt();
                    } else {
                        int value;
                        if (IntAsHex) {
                            value = IntFromHex(rawValue);
                        } else {
                            value = int.Parse(rawValue, NumberStyles.Integer);
                        }

                        node = new TagNodeInt(value);
                    }

                    break;

                case TagType.TAG_LONG:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeLong();
                    } else {
                        long value;
                        if (LongAsHex) {
                            value = LongFromHex(rawValue);
                        } else {
                            value = long.Parse(rawValue, NumberStyles.Integer);
                        }

                        node = new TagNodeLong(value);
                    }

                    break;

                case TagType.TAG_FLOAT:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeFloat();
                    } else {
                        float value;
                        value = float.Parse(rawValue, NumberStyles.Float);

                        node = new TagNodeFloat(value);
                    }

                    break;

                case TagType.TAG_DOUBLE:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeDouble();
                    } else {
                        double value;
                        value = double.Parse(rawValue, NumberStyles.Float);

                        node = new TagNodeDouble(value);
                    }

                    break;

                case TagType.TAG_BYTE_ARRAY:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeByteArray();
                    } else {
                        byte[] value;
                        
                        List<byte> bytes = new List<byte>();
                        
                        foreach (string str in rawValue.Split(ArraySep)) {
                            if (ByteArrayAsHex) {
                                bytes.Add(ByteFromHex(str));
                            } else {
                                bytes.Add(byte.Parse(str.Trim(), NumberStyles.Integer));
                            }
                        }

                        value = bytes.ToArray();

                        node = new TagNodeByteArray(value);
                    }

                    break;

                case TagType.TAG_STRING:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeString();
                    } else {
                        node = new TagNodeString(rawValue);
                    }

                    break;

                case TagType.TAG_INT_ARRAY:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeIntArray();
                    } else {
                        int[] value;
                        
                        List<int> ints = new List<int>();
                        
                        foreach (string str in rawValue.Split(ArraySep)) {
                            if (IntArrayAsHex) {
                                ints.Add(IntFromHex(str));
                            } else {
                                ints.Add(int.Parse(str.Trim(), NumberStyles.Integer));
                            }
                        }

                        value = ints.ToArray();

                        node = new TagNodeIntArray(value);
                    }

                    break;
                default:
                    throw new InvalidTagException();
            }
        }

        private string readTextElement(XmlReader reader) {
            string rt = null;

            if (reader.IsEmptyElement) {
                // Move on to the next element
                reader.Read();

                return null;
            }

            while (!reader.EOF) {
                reader.Read();

                if (reader.IsStartElement()) {
                    // Not expecting children
                    reader.Skip();
                } else if (XmlNodeType.EndElement == reader.NodeType) {
                    // Since we skip children this must be the end of our element
                    reader.ReadEndElement();
                    break;
                } else if (XmlNodeType.Text == reader.NodeType) {
                    rt = reader.Value;

                    // We have to keep reading until we get to the end of our element
                }
            }

            return rt;
        }
    }
}
