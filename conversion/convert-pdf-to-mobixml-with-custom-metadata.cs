using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputMobi = "output.mobi";
        const string author    = "John Doe";
        const string publisher = "Acme Publishing";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set standard metadata fields
            doc.Info.Author = author;

            // Add custom metadata (Publisher) – not a predefined key, so we add it manually
            doc.Info["Publisher"] = publisher;

            // Prepare MobiXml save options (default constructor is sufficient)
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document as MobiXml using the specified options
            doc.Save(outputMobi, mobiOptions);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: {outputMobi}");
    }
}