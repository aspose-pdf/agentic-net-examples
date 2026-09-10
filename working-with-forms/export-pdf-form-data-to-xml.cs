using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "formData.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export the document (including its form fields) to an XML file
            pdfDocument.SaveXml(outputXmlPath);
        }

        Console.WriteLine($"Form data exported successfully to '{outputXmlPath}'.");
    }
}