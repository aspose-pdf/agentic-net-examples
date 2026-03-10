using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document using the Document constructor (PDF input)
        // and ensure deterministic disposal with a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Save the document as PDF. Document.Save(string) without SaveOptions
            // always writes a PDF regardless of the file extension (save rule).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}