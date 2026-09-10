using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, MobiXmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output MobiXml file path
        const string outputMobiXmlPath = "output.mobixml";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and save it as MobiXml using default options
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize default MobiXml save options
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document in MobiXml format
            pdfDocument.Save(outputMobiXmlPath, mobiOptions);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: '{outputMobiXmlPath}'");
    }
}