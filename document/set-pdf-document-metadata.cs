using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF, modify its metadata, and save it.
        using (Document doc = new Document(inputPath))
        {
            // Set creation and modification dates.
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate      = DateTime.Now;

            // Set custom keywords (semicolon‑separated list is typical).
            doc.Info.Keywords = "example; Aspose.Pdf; metadata";

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with updated properties to '{outputPath}'.");
    }
}