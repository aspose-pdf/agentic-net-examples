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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set standard author metadata
            doc.Info.Author = author;

            // Set custom publisher metadata (no dedicated property, use dictionary entry)
            doc.Info["Publisher"] = publisher;

            // Save the document as MobiXml using explicit save options
            MobiXmlSaveOptions saveOptions = new MobiXmlSaveOptions();
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: {outputPath}");
    }
}