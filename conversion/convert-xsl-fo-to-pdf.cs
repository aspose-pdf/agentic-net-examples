using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.fo";   // XSL‑FO XML file
        const string outputPath = "output.pdf"; // Resulting PDF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the XSL‑FO document with default settings
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(); // defaults

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save as PDF (default format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}