using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace IconFontHelper.Classes
{
    public static class SerializationHelper
    {
        public static void SerializeCollectionToFile(List<GlyphInfo> collection, string file)
        {
            var serializer = new XmlSerializer(typeof(List<GlyphInfo>));

            using (var writer = new StreamWriter(file))
            {
                serializer.Serialize(writer, collection);
            }
        }

        public static List<GlyphInfo> DeserializeCollectionFromFile(string file)
        {
            List<GlyphInfo> result;
            var serializer = new XmlSerializer(typeof(List<GlyphInfo>));

            using (var reader = new StreamReader(file))
            {
                result = (List<GlyphInfo>)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
