using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string xslPath = "transform.xsl"; // XSL‑FO stylesheet
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL‑FO stylesheet not found: {xslPath}");
            return;
        }

        // Load XML with XSL‑FO transformation
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{pdfPath}'.");
    }
}