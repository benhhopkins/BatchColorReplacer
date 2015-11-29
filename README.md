# BatchColorReplacer
Batch replace specific color(s) in multiple images leveraging ImageMagick.

Arguments are a file of color replacement pairs along with any number image files. Wildcards may be used.

I use this to mass-replace color palettes across sprite images.

IMPORTANT:
Convert.exe, part of the free [ImageMagick](http://www.imagemagick.org/script/index.php) suite, is used to perform the color replacement. By default, convert.exe must exist at C:\Packages\ImageMagick.

Example Usages:
> BatchColorReplacer colorDefinitions.txt image1.png image2.png

> BatchColorReplacer colorDefinitions.txt image*.png

Example color definitions file:
>\#FFFFFF #FF0C00  
>\#A80000 #000000  
>\#FF00FF #FFFF00  
