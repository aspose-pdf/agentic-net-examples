using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "output.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize XML save options
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML
            pdfDoc.Save(outputXml, xmlOptions);
        }

        Console.WriteLine($"PDF successfully saved as XML to '{outputXml}'.");
    }
}