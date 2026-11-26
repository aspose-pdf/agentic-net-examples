using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xps";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create default XpsSaveOptions (no custom settings needed)
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS; explicit SaveOptions are required for non‑PDF formats
            doc.Save(outputPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully saved as XPS to '{outputPath}'.");
    }
}