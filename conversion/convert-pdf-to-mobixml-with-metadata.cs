using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.mobi";
        const string author = "John Doe";
        const string publisher = "Acme Publishing";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Set standard author metadata
                doc.Info.Author = author;

                // Add custom publisher metadata using the DocumentInfo indexer
                doc.Info["Publisher"] = publisher;

                // Prepare MobiXml save options (required for non‑PDF output)
                MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

                // Save the document as MobiXml using the explicit options
                doc.Save(outputPath, mobiOptions);
            }

            Console.WriteLine($"PDF successfully converted to MobiXml: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
