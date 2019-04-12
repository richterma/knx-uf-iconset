This tool will help with the creation of the "IconFontGenerator" batch.

The tool only features a few setting entries and two buttons.
The current code is not in a good state in any way. Lots of error checking and optimizations were skipped.
It is only intended as a little helper to create the batch file.

What will the tool do?

1. Create or update a list of glyphs. This list contains the position of the glyph within the font, the file name and a group id.
A pre-existing file is provided to keep compatibility with the "established" character numbering.
If a new "official" icon is added, a new entry to the xml should be added and the xml should be committed.

If someone wants to add a "private" icon, I'd suggest to place it before the existing icons.

When updating the xml, new icons are being added directly after the existing ones, in alphabetical order.
The group id is left blank for new entries. If an icon is part of an icon group, the xml must be edited manually.

Missing icons will not be removed from the xml.


2. Create a batch file based on the xml.
Each entry in the xml will be inspected using FontForge. The tool will check the bounding boxes of the glyphs and calculate how to position them "top-left".
If an icon is part of a group, additional handling is in place: The complete group is analysed for minimum movement and maximum width. These values are then used for all icons.
This handling allows to keep icons within a group in their relative position to each other.

Finally, all the information will be written to a batch file.
This batch file can then be used to create a font containing the glyphs.
Since each element is added individually, the final batch can be edited to include / exclude elements.