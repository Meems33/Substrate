
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

using Substrate.Nbt;

namespace Substrate.ImportExport {

    public class XmlDeserializer : XmlBase {

        public XmlDeserializer() {
        }

        public TagNode Deserialize(Stream inputStream) {
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

        public TagNode Deserialize(XmlReader reader) {
            return DeserializeStart(reader);
        }

        private TagNode DeserializeStart(XmlReader reader) {
            TagNode node = null;

            int numChildren = 0;
            while (!reader.EOF) {
                reader.Read();

                if (XmlNodeType.XmlDeclaration == reader.NodeType) {

                } else if (XmlNodeType.Element == reader.NodeType) {
                    TagType childType = getTagType(reader.Name);

                    
                    if (numChildren > 1) {
                        throw new InvalidTagException();
                    }

                    if (TagType.TAG_COMPOUND == childType) {
                        node = DeserializeCompound(reader).Item2;
                    } else if (TagType.TAG_LIST == childType) {
                        node = DeserializeList(reader).Item2;
                    } else {
                        node = DeserializeScalar(reader).Item2;
                    }

                    ++numChildren;
                }
            }

            return node;
        }

        // We just read a TAG_COMPOUND
        private Tuple<string, TagNode> DeserializeCompound(XmlReader reader) {
            TagNode node;
            string name;

            TagType type = getTagType(reader.Name);

            if (TagType.TAG_COMPOUND != type) {
                throw new InvalidTagException();
            }

            name = reader.GetAttribute("name");

            if (reader.IsEmptyElement) {
                node = new TagNodeCompound();
            } else {
                List<Tuple<string, TagNode>> gatheredTags = new List<Tuple<string, TagNode>>();

                while (!reader.EOF) {
                    reader.Read();

                    if (reader.IsStartElement()) {
                        // Child...
                        TagType childType = getTagType(reader.Name);

                        if (TagType.TAG_COMPOUND == childType) {
                            gatheredTags.Add(DeserializeCompound(reader));
                        } else if (TagType.TAG_LIST == childType) {
                            gatheredTags.Add(DeserializeList(reader));
                        } else {
                            gatheredTags.Add(DeserializeScalar(reader));
                        }
                    } else if (XmlNodeType.EndElement == reader.NodeType) {
                        // Children consume their own end elements so this must be ours
                        reader.ReadEndElement();
                        break;
                    }
                }

                TagNodeCompound tnc = new TagNodeCompound();

                gatheredTags.ForEach(innerNode => tnc.Add(innerNode.Item1, innerNode.Item2));

                node = tnc;
            }

            return new Tuple<string, TagNode>(name, node);
        }

        private Tuple<string, TagNode> DeserializeList(XmlReader reader) {
            TagNode node;
            string name;

            TagType type = getTagType(reader.Name);

            if (TagType.TAG_LIST != type) {
                throw new InvalidTagException();
            }

            name = reader.GetAttribute("name");
            string innerType = reader.GetAttribute("type");
            TagType innerTypeTag = getTagType(innerType);

            if (reader.IsEmptyElement) {
                node = new TagNodeList(innerTypeTag);
            } else {
                List<Tuple<string, TagNode>> gatheredTags = new List<Tuple<string, TagNode>>();

                while (!reader.EOF) {
                    reader.Read();

                    if (reader.IsStartElement()) {
                        // Child...
                        TagType childType = getTagType(reader.Name);

                        if (TagType.TAG_COMPOUND == childType) {
                            gatheredTags.Add(DeserializeCompound(reader));
                        } else if (TagType.TAG_LIST == childType) {
                            gatheredTags.Add(DeserializeList(reader));
                        } else {
                            gatheredTags.Add(DeserializeScalar(reader));
                        }
                    } else if (XmlNodeType.EndElement == reader.NodeType) {
                        // Children consume their own end elements so this must be ours
                        reader.ReadEndElement();
                        break;
                    }
                }

                List<TagNode> listItems = new List<TagNode>();
                gatheredTags.ForEach(innerNode => listItems.Add(innerNode.Item2));
                
                if (listItems == null) {
                    node = new TagNodeList(innerTypeTag);
                } else {
                    node = new TagNodeList(innerTypeTag, listItems);
                }
            }

            return new Tuple<string, TagNode>(name, node);
        }

        private Tuple<string, TagNode> DeserializeScalar(XmlReader reader) {
            // TODO add validation

            TagType type = getTagType(reader.Name);

            // Let's read the attr if it exists
            string name = reader.GetAttribute("name");
            string rawValue = readTextElement(reader);
            TagNode node;

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
                    // TODO convert to UTF-8?
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeString();
                    } else {
                        node = new TagNodeString(rawValue);
                    }

                    break;

                case TagType.TAG_INT_ARRAY:
                    if (rawValue == null || rawValue.Trim().Length == 0) {
                        node = new TagNodeByteArray();
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

            return new Tuple<string, TagNode>(name, node);
        }

        private string readTextElement(XmlReader reader) {
            string rt = null;

            if (reader.IsEmptyElement) {
                return rt;
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
