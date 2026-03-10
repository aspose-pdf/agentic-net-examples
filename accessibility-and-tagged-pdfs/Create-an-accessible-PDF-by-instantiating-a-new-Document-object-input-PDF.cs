using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF using a FileStream and the Document(Stream) constructor
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (Document doc = new Document(inputStream))
        {
            // The document is now loaded; additional accessibility steps could be added here
            // (e.g., enable auto‑tagging, set language, add structure elements, etc.)

            // Save the document back as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}