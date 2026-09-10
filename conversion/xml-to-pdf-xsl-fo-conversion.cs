using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSL‑FO stylesheet and the output PDF.
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

        // Create load options that reference the XSL‑FO stylesheet.
        // The XmlLoadOptions constructor that accepts a string sets the XSL stream.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFile);

        // Load the XML file with the XSL‑FO transformation applied and save as PDF.
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF generated successfully: {pdfFile}");
    }
}