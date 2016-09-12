using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace GenericExtensions
{
    public static class XmlExtensions
    {
        public static T Deserialize<T>(this string xml)
        {
            T returnedXmlClass = default(T);

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass =
                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return returnedXmlClass;
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);

                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);

                return string.Empty;
            }
        }
    }
}
