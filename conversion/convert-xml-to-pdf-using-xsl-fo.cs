using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source XML, XSL‑FO stylesheet and the output PDF.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Paths to the XML input, the XSL‑FO stylesheet and the resulting PDF.
        string xmlPath = Path.Combine(dataDir, "input.xml");
        string xslPath = Path.Combine(dataDir, "transform.xsl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

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

        // Load the XML file and apply the XSL‑FO transformation.
        // XmlLoadOptions can be constructed with the XSL‑FO file path.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Use a using block to ensure the Document is disposed properly.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at: {pdfPath}");
    }
}