using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "formdata.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export the document (including AcroForm data) to XML
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDocument.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"AcroForm data exported to XML file: {outputXmlPath}");
    }
}