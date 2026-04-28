using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_blank_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            doc.Pages.Add();

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added and saved to '{outputPath}'.");
    }
}