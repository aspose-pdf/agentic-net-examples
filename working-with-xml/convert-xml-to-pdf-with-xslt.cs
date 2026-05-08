using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSLT stylesheet, and the output PDF.
        const string xmlPath = "input.xml";
        const string xslPath = "transform.xsl";
        const string pdfPath = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslPath}");
            return;
        }

        // Create XmlLoadOptions that reference the XSLT file.
        // This tells Aspose.Pdf to apply the stylesheet while loading the XML.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML with the specified options and convert it to PDF.
        // The Document constructor with (string, LoadOptions) follows the lifecycle rule.
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are needed for PDF output.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}