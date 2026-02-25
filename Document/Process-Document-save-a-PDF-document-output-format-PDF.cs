using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and all related types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // destination PDF file

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Lifecycle rule: always wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Save the document in PDF format (default). No SaveOptions needed.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}