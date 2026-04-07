using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML data, the XSL‑FO template, and the resulting PDF.
        const string xmlPath   = "data.xml";
        const string xslFoPath = "template.xslfo";
        const string pdfPath   = "output.pdf";

        // Verify that the required input files exist.
        if (!File.Exists(xmlPath) || !File.Exists(xslFoPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Create load options that associate the XSL‑FO stylesheet with the XML data.
        // The XmlLoadOptions constructor accepting a string sets the XSL‑FO stream internally.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFoPath);

        // Load the XML document, applying the XSL‑FO template, and generate a PDF.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF to the specified path.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}