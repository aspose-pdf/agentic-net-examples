using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSL‑FO stylesheet, and the output PDF.
        const string xmlPath   = "input.xml";
        const string xslPath   = "template.xslfo";   // XSL‑FO (XSL) file used for transformation
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

        // Initialise load options with the XSL‑FO stylesheet.
        // XmlLoadOptions(string) constructor accepts the XSL file path.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML file and apply the XSL‑FO transformation.
        // The Document constructor takes the XML file path and the load options.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}