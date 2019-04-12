namespace IconFontHelper.Classes
{
    public class ExtendedGlyphInfo
    {
        public GlyphInfo Info { get; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        public int Width { get; set; }
        public int ScalingFactor { get; set; }

        public ExtendedGlyphInfo(GlyphInfo info)
        {
            Info = info;
        }

        public ExtendedGlyphInfo(GlyphInfo info, int deltaX, int deltaY, int width, int scalingFactor)
        {
            Info = info;
            DeltaX = deltaX;
            DeltaY = deltaY;
            Width = width;
            ScalingFactor = scalingFactor;
        }
    }
}
