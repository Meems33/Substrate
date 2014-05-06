using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

using Substrate.Nbt;

namespace Substrate.ImportExport {
    /// <summary>
    /// Serializes TagNodes to XML
    /// </summary>
    public class XmlSerializer : XmlBase {

        /// <summary>
        /// Creates an XmlSerializer
        /// </summary>
        /// <param name="textWriter">The stream to write to</param>
        public XmlSerializer() {
        }

        /// <summary>
        /// Serialize a TagNode to XML
        /// </summary>
        /// <param name="tag"></param>
        public void Serialize(TextWriter writer, TagNode tag) {
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.Encoding = System.Text.Encoding.UTF8;

            XmlWriter xmlWriter = XmlWriter.Create(writer, xmlSettings);

            try {
                Serialize(xmlWriter, tag);

                writer.Flush();
            } finally {
                if (writer != null) {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Serialize a TagNode to XML onto the XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="tag"></param>
        public void Serialize(XmlWriter writer, TagNode tag) {
            SerializeStart(writer, tag);
        }

        protected void SerializeStart(XmlWriter writer, TagNode tag) {
            bool writeHeader = !writer.Settings.OmitXmlDeclaration;

            if (writeHeader) {
                writer.WriteStartDocument();
            }

            if (tag != null) {
                if (tag.GetTagType() == TagType.TAG_COMPOUND) {
                    SerializeCompound(writer, tag as TagNodeCompound, null);
                } else if (tag.GetTagType() == TagType.TAG_LIST) {
                    SerializeList(writer, tag as TagNodeList, null);
                } else {
                    SerializeScalar(writer, tag, null);
                }

            }

            if (writeHeader) {
                writer.WriteEndDocument();
            }
        }

        protected void SerializeCompound(XmlWriter writer, TagNodeCompound tag, string name) {
            writer.WriteStartElement(getTagName(tag));

            if (name != null) {
                writer.WriteAttributeString("name", name);
            }

            foreach (KeyValuePair<string, TagNode> pair in tag) {
                TagNode item = pair.Value;

                // Compounds give inner elements names
                if (item.GetTagType() == TagType.TAG_COMPOUND) {
                    SerializeCompound(writer, item as TagNodeCompound, pair.Key);
                } else if (item.GetTagType() == TagType.TAG_LIST) {
                    SerializeList(writer, item as TagNodeList, pair.Key);
                } else {
                    SerializeScalar(writer, item, pair.Key);
                }
            }

            writer.WriteEndElement();
        }

        protected void SerializeList(XmlWriter writer, TagNodeList tag, string name) {
            writer.WriteStartElement(getTagName(tag));

            if (name != null) {
                writer.WriteAttributeString("name", name);
            }

            writer.WriteAttributeString("type", getTagName(tag.ValueType));

            foreach (TagNode item in tag) {
                // Lists do not give inner elements names
                if (item.GetTagType() == TagType.TAG_COMPOUND) {
                    SerializeCompound(writer, item as TagNodeCompound, null);
                } else if (item.GetTagType() == TagType.TAG_LIST) {
                    SerializeList(writer, item as TagNodeList, null);
                } else {
                    SerializeScalar(writer, item, null);
                }
            }

            writer.WriteEndElement();
        }

        protected void SerializeScalar(XmlWriter writer, TagNode tag, string name) {
            writer.WriteStartElement(getTagName(tag));

            if (name != null) {
                writer.WriteAttributeString("name", name);
            }

            switch (tag.GetTagType()) {
                case TagType.TAG_END:
                    // We just write an empty tag
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
                    }
                    break;

                case TagType.TAG_STRING:
                    // TODO Strings are special... lookup null vs ""?
                    if (tag.ToTagString().Data != null) {
                        byte[] bytes = Encoding.Default.GetBytes(tag.ToTagString().Data);
                        writer.WriteValue(Encoding.UTF8.GetString(bytes));
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
                    }
                    break;

                default:
                    throw new InvalidTagException();
            }

            writer.WriteEndElement();
        }
    }
}
