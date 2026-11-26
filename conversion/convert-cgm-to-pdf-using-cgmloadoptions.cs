using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.cgm";
        const string outputPath = "output.pdf";

        // Verify that the source CGM file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the CGM using CgmLoadOptions (CGM is input‑only)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the loaded content as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"CGM converted to PDF: {outputPath}");
    }
}