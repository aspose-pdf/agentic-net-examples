using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add custom key‑value pairs to the document information dictionary.
            // These entries can hold XML fragments or any other data required downstream.
            doc.Info.Add("CustomXml", "<data><item id=\"1\">Value</item></data>");
            doc.Info.Add("ProcessingFlag", "true");

            // Save the modified document. No additional SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}