using System;
using System.Globalization;

namespace Substrate.Nbt {
    public class Tuple<T1, T2> { 
        public Tuple(T1 item1, T2 item2) { 
            Item1 = item1;
            Item2 = item2; 
        } 
    
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }  
    }

    public class XmlBase {
        public char ArraySep { get; set;  }
       
        public bool ByteAsHex { get; set; }
        public bool ShortAsHex{ get; set; }
        public bool IntAsHex { get; set; }
        public bool LongAsHex { get; set; }
        public bool ByteArrayAsHex { get; set; }
        public bool IntArrayAsHex { get; set; }

        protected XmlBase() {
            ArraySep = ' ';

            ByteAsHex = false;
            ShortAsHex = false;
            IntAsHex = false;
            LongAsHex = false;
            ByteArrayAsHex = true;
            IntArrayAsHex = true;
        }

        protected string getTagName(TagNode tag) {
            if (tag == null || tag.GetTagType() == null) {
                throw new NullReferenceException();
            }

            return getTagName(tag.GetTagType());
        }

        protected string getTagName(TagType tagType) {
            return Enum.GetName(typeof(TagType), tagType);
        }

        protected TagType getTagType(string tagName) {
            try {
                return (TagType)Enum.Parse(typeof(TagType), tagName);
            } catch (Exception e) {
                throw new InvalidTagException("Unrecognized tag type: " + tagName);
            }
        }

        protected bool isScalar(TagType tagType) {
            switch (tagType) {
                case TagType.TAG_END:
                case TagType.TAG_BYTE:
                case TagType.TAG_SHORT:
                case TagType.TAG_INT:
                case TagType.TAG_LONG:
                case TagType.TAG_FLOAT:
                case TagType.TAG_DOUBLE:
                case TagType.TAG_BYTE_ARRAY:
                case TagType.TAG_STRING:
                case TagType.TAG_INT_ARRAY:
                    return true;
            }

            return false;
        }

        protected bool isTagType(string str) {
            try {
                getTagType(str);
                return true;
            } catch (InvalidTagException e) {
                return false;
            }
        }

        protected string ByteToHex(byte b) {
            return b.ToString("X2");
        }

        protected byte ByteFromHex(string s) {
            try {
                return byte.Parse(s, NumberStyles.HexNumber);
            } catch (Exception) {
                throw new InvalidValueException("Cannot parse value to byte: " + s);
            }
        }

        protected string ShortToHex(short s) {
            return s.ToString("X4");
        }

        protected short ShortFromHex(string s) {
            try {
                return short.Parse(s, NumberStyles.HexNumber);
            } catch (Exception) {
                throw new InvalidValueException("Cannot parse value to short: " + s);
            }
        }

        protected string IntToHex(int i) {
            return i.ToString("X8");
        }

        protected int IntFromHex(string s) {
            try {
                return int.Parse(s, NumberStyles.HexNumber);
            } catch (Exception) {
                throw new InvalidValueException("Cannot parse value to int: " + s);
            }
        }

        protected string LongToHex(long l) {
            return l.ToString("X16");
        }

        protected long LongFromHex(string s) {
            try {
                return long.Parse(s, NumberStyles.HexNumber);
            } catch (Exception) {
                throw new InvalidValueException("Cannot parse value to long: " + s);
            }
        }
    }
}
