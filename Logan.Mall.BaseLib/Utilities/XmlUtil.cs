using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace Logan.Mall.BaseLib.Utilities
{
    public class XmlUtil
    {
        public static T LoadFromXml<T>(string filePath) where T : class
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(fs) as T;
            }
        }

        public static T LoadFromMessage<T>(string xmlMessage) where T : class
        {
            using (StringReader sr = new StringReader(xmlMessage))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(sr) as T;
            }
        }

        public static string ToXmlMessage<T>(T t) where T : class
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(xw, t);
                return sb.ToString();
            }

        }

        public static void Save<T>(T t, string relativePath) where T : class
        {
            string filePath = PathUtil.GetFilePath(relativePath);
            SaveAs<T>(t, filePath);
        }

        public static void SaveAs<T>(T t, string filePath) where T : class
        {
            using (XmlWriter xw = XmlWriter.Create(filePath))
            {
                XmlSerializer serilizer = new XmlSerializer(typeof(T));
                serilizer.Serialize(xw, t);
            }
        }

    }
}
