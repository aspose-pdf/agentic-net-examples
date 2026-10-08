using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "ExtractedFonts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Try the modern FontsInfo property via reflection (available in newer versions).
            PropertyInfo fontsInfoProp = typeof(Document).GetProperty("FontsInfo");
            if (fontsInfoProp != null)
            {
                // The property returns a collection of FontInfo objects.
                IEnumerable fontsInfo = fontsInfoProp.GetValue(pdfDoc) as IEnumerable;
                if (fontsInfo != null)
                {
                    foreach (object fontInfoObj in fontsInfo)
                    {
                        // Use dynamic to avoid compile‑time dependency on the FontInfo type.
                        dynamic fontInfo = fontInfoObj;
                        byte[] fontData = fontInfo.FontFile as byte[];
                        if (fontData == null || fontData.Length == 0)
                            continue; // Skip non‑embedded fonts.

                        string fontName = (fontInfo.FontName as string) ?? "UnnamedFont";
                        string fontType = (fontInfo.FontType as string) ?? string.Empty;

                        string extension = ".ttf"; // default
                        if (!string.IsNullOrEmpty(fontType) &&
                            fontType.Equals("OpenType", StringComparison.OrdinalIgnoreCase))
                        {
                            extension = ".otf";
                        }

                        string safeFontName = string.Join("_",
                            fontName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

                        string outputPath = Path.Combine(outputDir, safeFontName + extension);
                        File.WriteAllBytes(outputPath, fontData);
                        Console.WriteLine($"Extracted font: {fontName} -> {outputPath}");
                    }
                }
            }
            else
            {
                // Fallback for older versions – iterate over page resources.
                foreach (Page page in pdfDoc.Pages)
                {
                    // page.Resources.Fonts is a collection of Font objects.
                    foreach (Font font in page.Resources.Fonts)
                    {
                        // FontFile may be null for non‑embedded fonts.
                        byte[] fontData = null;
                        // Use reflection because the exact Font subclass (TrueTypeFont, Type1Font, etc.)
                        // may expose the FontFile property differently.
                        PropertyInfo fontFileProp = font.GetType().GetProperty("FontFile");
                        if (fontFileProp != null)
                        {
                            fontData = fontFileProp.GetValue(font) as byte[];
                        }
                        if (fontData == null || fontData.Length == 0)
                            continue;

                        // Try to obtain a readable name.
                        string fontName = "UnnamedFont";
                        PropertyInfo nameProp = font.GetType().GetProperty("FontName");
                        if (nameProp != null)
                        {
                            fontName = nameProp.GetValue(font) as string ?? fontName;
                        }

                        // Determine extension – most embedded fonts are TrueType (ttf) or OpenType (otf).
                        string extension = ".ttf";
                        PropertyInfo typeProp = font.GetType().GetProperty("FontType");
                        if (typeProp != null)
                        {
                            string typeVal = typeProp.GetValue(font) as string;
                            if (!string.IsNullOrEmpty(typeVal) &&
                                typeVal.Equals("OpenType", StringComparison.OrdinalIgnoreCase))
                            {
                                extension = ".otf";
                            }
                        }

                        string safeFontName = string.Join("_",
                            fontName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
                        string outputPath = Path.Combine(outputDir, safeFontName + extension);
                        File.WriteAllBytes(outputPath, fontData);
                        Console.WriteLine($"Extracted font (fallback): {fontName} -> {outputPath}");
                    }
                }
            }
        }

        Console.WriteLine("Font extraction completed.");
    }
}
