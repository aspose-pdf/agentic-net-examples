using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSL‑FO stylesheet and the output PDF.
        const string xmlPath   = "input.xml";
        const string xslPath   = "template.xslfo";
        const string pdfPath   = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslPath}");
            return;
        }

        try
        {
            // Load the XML document and apply the XSL‑FO stylesheet.
            // XmlLoadOptions constructor accepts the XSL‑FO file which will be used
            // to transform the XML into a PDF layout.
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

            // The Document constructor loads the XML together with the XSL‑FO
            // transformation options and creates an in‑memory PDF representation.
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF to the specified path.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}