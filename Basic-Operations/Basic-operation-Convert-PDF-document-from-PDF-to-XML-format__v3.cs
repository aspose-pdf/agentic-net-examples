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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (creation)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize XML save options (creation)
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML (saving)
            pdfDoc.Save(outputXml, xmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to XML: {outputXml}");
    }
}