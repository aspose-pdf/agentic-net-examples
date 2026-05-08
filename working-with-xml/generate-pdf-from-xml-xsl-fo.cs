using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSL‑FO template and the output PDF.
        const string xmlPath      = "input.xml";
        const string xslFoPath    = "template.xslfo";
        const string outputPdfPath = "result.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO template not found: {xslFoPath}");
            return;
        }

        // Load the XML file and apply the XSL‑FO template.
        // XmlLoadOptions(string) constructor sets the XSL‑FO file used for transformation.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFoPath);

        // The Document constructor takes the XML file and the load options.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{outputPdfPath}'.");
    }
}