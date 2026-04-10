using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML data, the XSL‑FO stylesheet and the output PDF.
        const string xmlFile   = "input.xml";
        const string xslFile   = "template.xslfo";
        const string pdfFile   = "output.pdf";

        // Verify that the input files exist.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }
        if (!File.Exists(xslFile))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFile}");
            return;
        }

        // XmlLoadOptions can be constructed with the XSL‑FO file.
        // This tells Aspose.Pdf to apply the XSL‑FO transformation while loading the XML.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFile);

        // Load the XML and convert it to PDF in a using block (ensures proper disposal).
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF generated successfully: {pdfFile}");
    }
}