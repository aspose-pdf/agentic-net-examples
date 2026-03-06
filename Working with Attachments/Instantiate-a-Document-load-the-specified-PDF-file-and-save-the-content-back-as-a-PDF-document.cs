using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Path where the PDF will be saved (can be the same as inputPath to overwrite)
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document, process it, and save it back as PDF
        // Document implements IDisposable, so we wrap it in a using block for deterministic cleanup
        using (Document doc = new Document(inputPath))
        {
            // No modifications are performed; simply save the loaded content
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}