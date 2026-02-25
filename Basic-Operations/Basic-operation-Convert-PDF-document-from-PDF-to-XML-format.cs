using System;
using System.IO;
using Aspose.Pdf; // XmlSaveOptions is in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputXml = "output.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize XML save options (no special settings required)
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the PDF as XML using the options
            pdfDocument.Save(outputXml, xmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to XML: {outputXml}");
    }
}