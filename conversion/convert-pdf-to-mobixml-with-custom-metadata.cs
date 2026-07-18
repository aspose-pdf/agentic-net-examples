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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set standard author metadata
            doc.Info.Author = author;

            // Set custom publisher metadata (no dedicated property, use custom key)
            doc.Info["Publisher"] = publisher;

            // Create MobiXml save options (default constructor)
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document as MobiXml using the options
            doc.Save(outputMobi, mobiOptions);
        }

        Console.WriteLine($"PDF converted to MobiXml with metadata saved to '{outputMobi}'.");
    }
}