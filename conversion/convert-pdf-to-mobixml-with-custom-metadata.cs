using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.mobi.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set standard author metadata
            doc.Info.Author = "John Doe";

            // Add custom publisher metadata (no dedicated property, use custom key)
            doc.Info.Add("Publisher", "Acme Publishing");

            // Create MobiXml save options (explicit options are required for non‑PDF formats)
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document as MobiXml using the explicit save options
            doc.Save(outputPath, mobiOptions);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: {outputPath}");
    }
}