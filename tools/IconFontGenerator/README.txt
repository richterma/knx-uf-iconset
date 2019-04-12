This script will create a ttf font file with the selected icons.

However, the process is not completely automated yet.
Some of the icons contain "invisible elements".
During the creation of the font file, the visibility will be ignored, hence some icons will not display properly (show additional elements).

To work around this issue, simply run all of the icons trough "svgcleaner" ( https://github.com/RazrFalcon/svgcleaner ).
Store the processed icons in the folder "optimized_svg" and the script will automatically use them instead of the raw files.


Further prerequisites:
1. The script relies on the original folder structure.
2. The script relies on FontForge ( https://FontForge.github.io ). The FontForge binary must be available and the path must be configured in the script. Please check the beginning of the script for the configuration part.


Usage:
1. Make sure prerequisites are met and (optionally) run all icons trough svgcleaner.
2. Edit the script file to configure the correct FontForge path.
3. By default, the script will not include any icon in the font. To include icons, remove the comments (the "::" at the begonning of each line) from each icon you want to include in the font. You should only include the required files to minimize font size and loading time.