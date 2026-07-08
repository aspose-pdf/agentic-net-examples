using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Facades;            // For any facade utilities (not used here)

class ReplaceVectorWithRaster
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing vector graphic
        const string outputPath = "output.pdf";  // Resulting PDF with rasterized graphic

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor inside using)
        using (Document doc = new Document(inputPath))
        {
            // FlattenTransparency replaces vector graphics (and transparent content)
            // with raster equivalents while keeping the original page layout intact.
            doc.FlattenTransparency();

            // Save the modified document (lifecycle rule: use Document.Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphics have been rasterized and saved to '{outputPath}'.");
    }
}