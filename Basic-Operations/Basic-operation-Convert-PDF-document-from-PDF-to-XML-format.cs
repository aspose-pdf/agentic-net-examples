using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXml = "output.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Specify XML save options to correctly produce an XML file
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML using the options
            pdfDocument.Save(outputXml, xmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to XML: {outputXml}");
    }
}