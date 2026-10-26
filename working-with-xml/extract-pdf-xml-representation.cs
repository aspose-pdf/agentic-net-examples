using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputXmlPath = "output.xml";     // XML representation

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // XmlSaveOptions forces the output format to XML
            XmlSaveOptions xmlOpts = new XmlSaveOptions();

            // Save the PDF as XML. The overload with SaveOptions is required;
            // otherwise the file would be saved as PDF regardless of extension.
            pdfDoc.Save(outputXmlPath, xmlOpts);
        }

        Console.WriteLine($"XML representation saved to '{outputXmlPath}'.");
    }
}