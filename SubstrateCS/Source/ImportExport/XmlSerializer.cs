using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

using Substrate.Nbt;

namespace Substrate.ImportExport {

    /// <summary>
    /// Serializes TagNodes into XML
    /// 
    /// Note the following:
    /// - It is assumed that TagNodeList and TagNodeCompound tags have proper internal
    ///   data representions (not null and not containing null tag nodes).
    /// - NBT format does not differentiate between "null" and empty objects. As a result,
    ///   TagNodeByteArray, TagNodeIntArray, and TagNodeString will serialize into the same
    ///   form for both when the internal data is null and the internal object is empty.
    /// - The XmlBase class defines properties outlining whether certain tag nodes are
    ///   serialized in decimal or hexadecimal form.
    ///   
    /// The XML format is described in NbtXml.xsd
    /// </summary>
    public class XmlSerializer : XmlBase {

        /// <summary>
        /// Creates an XmlSerializer
        /// </summary>
        public XmlSerializer() {
        }

        /// <summary>
        /// Serializes a TagNode into XML using default XML settings.
        ///
        /// </summary>
        /// <param name="writer">The stream to write to. Closed after use.</param>
        /// <param name="tag">The TagNode to serialize</param>
        public void Serialize(Stream writer, TagNode tag) {
            if (writer == null || tag == null) {
                throw new ArgumentNullException();
            }

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.Encoding = System.Text.Encoding.UTF8;

            XmlWriter xmlWriter = XmlWriter.Create(writer, xmlSettings);

            try {
                Serialize(xmlWriter, tag);

                xmlWriter.Flush();
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }
        }

        /// <summary>
        /// Serializes a TagNode into XML. The XmlWriter is
        /// NOT closed upon completion.
        /// </summary>
        /// <param name="writer">The XMLWriter to use</param>
        /// <param name="tag">The TagNode to serialize</param>
        public void Serialize(XmlWriter writer, TagNode tag) {
            if (writer == null || tag == null) {
                throw new ArgumentNullException();
            }

            SerializeStart(writer, tag);
        }

        protected void SerializeStart(XmlWriter writer, TagNode tag) {
            //Console.WriteLine("SerializeStart");
            bool writeHeader = !writer.Settings.OmitXmlDeclaration;

            if (writeHeader) {
                writer.WriteStartDocument();
            }

            if (tag.GetTagType() == TagType.TAG_COMPOUND) {
                SerializeCompound(writer, tag as TagNodeCompound, null);
            } else if (tag.GetTagType() == TagType.TAG_LIST) {
                SerializeList(writer, tag as TagNodeList, null);
            } else {
                SerializeScalar(writer, tag, null);
            }

            // Force the end element to show up
            writer.WriteWhitespace("");

            if (writeHeader) {
                writer.WriteEndDocument();
            }
        }

        protected void SerializeCompound(XmlWriter writer, TagNodeCompound tag, string name) {
            //Console.WriteLine("SerializeCompound");
            writer.WriteStartElement(getTagName(tag));

            if (name != null) {
                writer.WriteAttributeString("name", name);
            }

            foreach (KeyValuePair<string, TagNode> pair in tag) {
                TagNode item = pair.Value;

                if (item == null) {
                    throw new ArgumentNullException();
                }

                // Compounds give inner elements names
                if (item.GetTagType() == TagType.TAG_COMPOUND) {
                    SerializeCompound(writer, item as TagNodeCompound, pair.Key);
                } else if (item.GetTagType() == TagType.TAG_LIST) {
                    SerializeList(writer, item as TagNodeList, pair.Key);
                } else {
                    SerializeScalar(writer, item, pair.Key);
                }
            }

            // Force the end element to show up
            writer.WriteWhitespace("");

            writer.WriteEndElement();
        }

        protected void SerializeList(XmlWriter writer, TagNodeList tag, string name) {
            //Console.WriteLine("SerializeList");
            writer.WriteStartElement(getTagName(tag));

            if (name != null) {
                writer.WriteAttributeString("name", name);
            }

            writer.WriteAttributeString("type", getTagName(tag.ValueType));

            foreach (TagNode item in tag) {
                if (item == null) {
                    throw new ArgumentNullException();
                }

                // Lists do not give inner elements names
                if (item.GetTagType() == TagType.TAG_COMPOUND) {
                    SerializeCompound(writer, item as TagNodeCompound, null);
                } else if (item.GetTagType() == TagType.TAG_LIST) {
                    SerializeList(writer, item as TagNodeList, null);
                } else {
                    SerializeScalar(writer, item, null);
                }
            }

            // Force the end element to show up
            writer.WriteWhitespace("");

            writer.WriteEndElement();
        }

        protected void SerializeScalar(XmlWriter writer, TagNode tag, string name) {
            //Console.WriteLine("SerializeScalar");
            writer.WriteStartElement(getTagName(tag));

            if (name != null) {
                writer.WriteAttributeString("name", name);
            }

            switch (tag.GetTagType()) {
                case TagType.TAG_END:
                    // Force the end element to show up
                    writer.WriteWhitespace("");
                    break;

                case TagType.TAG_BYTE:
                    if (ByteAsHex) {
                        writer.WriteValue(ByteToHex(tag.ToTagByte().Data));
                    } else {
                        writer.WriteValue(tag.ToTagByte().Data);
                    }
                    break;

                case TagType.TAG_SHORT:
                    if (ShortAsHex) {
                        writer.WriteValue(ShortToHex(tag.ToTagShort().Data));
                    } else {
                        writer.WriteValue(tag.ToTagShort().Data);
                    }
                    break;

                case TagType.TAG_INT:
                    if (IntAsHex) {
                        writer.WriteValue(IntToHex(tag.ToTagInt().Data));
                    } else {
                        writer.WriteValue(tag.ToTagInt().Data);
                    }
                    break;

                case TagType.TAG_LONG:
                    if (LongAsHex) {
                        writer.WriteValue(LongToHex(tag.ToTagLong().Data));
                    } else {
                        writer.WriteValue(tag.ToTagLong().Data);
                    }
                    break;

                case TagType.TAG_FLOAT:
                    writer.WriteValue(tag.ToTagFloat().Data);
                    break;

                case TagType.TAG_DOUBLE:
                    writer.WriteValue(tag.ToTagDouble().Data);
                    break;

                case TagType.TAG_BYTE_ARRAY:
                    if (tag.ToTagByteArray().Data != null) {
                        StringBuilder sb = new StringBuilder();

                        bool first = true;
                        foreach (byte b in tag.ToTagByteArray().Data) {
                            if (!first) {
                                sb.Append(ArraySep);
                            }

                            if (ByteArrayAsHex) {
                                sb.Append(ByteToHex(b));
                            } else {
                                sb.Append(b);
                            }

                            first = false;
                        }

                        writer.WriteValue(sb.ToString());
                    } else {
                        // Force the end element to show up
                        writer.WriteWhitespace("");
                    }
                    break;

                case TagType.TAG_STRING:
                    if (tag.ToTagString().Data != null) {
                        byte[] bytes = Encoding.Default.GetBytes(tag.ToTagString().Data);
                        writer.WriteValue(Encoding.UTF8.GetString(bytes));
                    } else {
                        // Force the end element to show up
                        writer.WriteWhitespace("");
                    }
                    break;

                case TagType.TAG_INT_ARRAY:
                    if (tag.ToTagIntArray().Data != null) {
                        StringBuilder sb = new StringBuilder();

                        bool first = true;
                        foreach (int i in tag.ToTagIntArray().Data) {
                            if (!first) {
                                sb.Append(ArraySep);
                            }

                            if (IntArrayAsHex) {
                                sb.Append(IntToHex(i));
                            } else {
                                sb.Append(i);
                            }

                            first = false;
                        }

                        writer.WriteValue(sb.ToString());
                    } else {
                        // Force the end element to show up
                        writer.WriteWhitespace("");
                    }
                    break;

                default:
                    throw new InvalidTagException();
            }

            writer.WriteEndElement();
        }
    }
}
