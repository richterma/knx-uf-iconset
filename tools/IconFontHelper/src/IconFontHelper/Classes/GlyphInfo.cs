using System.Xml.Serialization;

namespace IconFontHelper.Classes
{
    [XmlRoot("GlyphInfo")]
    public class GlyphInfo
    {
        [XmlAttribute]
        public string FileName { get; set; }
        [XmlAttribute]
        public int CharId { get; set; }
        [XmlAttribute]
        public string GroupId { get; set; }

        public GlyphInfo() { }

        public GlyphInfo(string fileName, int charId, string groupId)
        {
            FileName = fileName;
            CharId = charId;
            GroupId = groupId;
        }
    }
}
