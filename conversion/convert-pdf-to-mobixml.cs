using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.mobi";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize MobiXmlSaveOptions with default settings
            var saveOptions = new MobiXmlSaveOptions();

            // Save the document as MobiXml using the save options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: {outputPath}");
    }
}
