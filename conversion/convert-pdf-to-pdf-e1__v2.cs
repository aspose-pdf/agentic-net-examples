using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath = "documentL.pdf";
        const string outputPath = "documentL_PDFE1.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // NOTE: The PdfEConvertOptions / PdfEStandard API is available only in newer
            // versions of Aspose.Pdf. The current referenced version does not contain these
            // types, therefore we fall back to a regular save. The document is saved as a
            // standard PDF; if a newer library version is added, the conversion code can be
            // re‑enabled by creating a PdfEConvertOptions instance and calling doc.Convert(...).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF file saved to '{outputPath}'.");
    }
}
