using System;
using System.IO;
using Aspose.Pdf;   // Document, XmlLoadOptions, etc.

class XmlToPdfWithXslFo
{
    static void Main()
    {
        // Paths to the source XML, the XSL‑FO stylesheet, and the output PDF.
        const string xmlFile   = "input.xml";
        const string xslFoFile = "template.xslfo";
        const string pdfFile   = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }
        if (!File.Exists(xslFoFile))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoFile}");
            return;
        }

        // Create load options that contain the XSL‑FO stylesheet.
        // XmlLoadOptions(string) constructor loads the XSL data for transformation.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFoFile);

        // Load the XML file with the XSL‑FO options and convert it to a PDF document.
        // The Document constructor applies the XSL‑FO transformation automatically.
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"XML has been transformed to PDF: {pdfFile}");
    }
}