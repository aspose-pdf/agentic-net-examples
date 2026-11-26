using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.djvu";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the DJVU image using DjvuLoadOptions
        DjvuLoadOptions loadOptions = new DjvuLoadOptions();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the document as PDF (default format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed: '{inputPath}' → '{outputPath}'");
    }
}