using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Save options like SvgSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string tempSvgPath = "temp.svg";
        const string outputSvgPath = "brand_palette.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // 1. Load the PDF inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // 2. Save the PDF as SVG using explicit SvgSaveOptions (save-to-non-pdf rule)
            var svgOptions = new SvgSaveOptions();
            pdfDoc.Save(tempSvgPath, svgOptions);
        }

        // 3. Load the generated SVG text
        string svgContent = File.ReadAllText(tempSvgPath);

        // 4. Define brand palette color replacements (old HEX -> new HEX)
        var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "#000000", "#1A73E8" }, // black → brand blue
            { "#FFFFFF", "#F1F3F4" }, // white → light gray
            { "#FF0000", "#EA4335" }  // red   → brand red
            // Add more mappings as needed
        };

        // 5. Perform simple string replacements for each color entry
        foreach (var kvp in colorMap)
        {
            // Replace both uppercase and lowercase occurrences
            svgContent = svgContent.Replace(kvp.Key, kvp.Value);
            // Also replace the lowercase version if the source uses it
            string lowerKey = kvp.Key.ToLowerInvariant();
            string lowerVal = kvp.Value.ToLowerInvariant();
            svgContent = svgContent.Replace(lowerKey, lowerVal);
        }

        // 6. Write the transformed SVG to the final output file
        File.WriteAllText(outputSvgPath, svgContent);

        // 7. Clean up the temporary SVG file
        try { File.Delete(tempSvgPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"SVG with brand palette saved to '{outputSvgPath}'.");
    }
}
