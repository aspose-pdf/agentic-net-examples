using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, XSLT stylesheet and the output PDF.
        const string xmlPath   = "input.xml";
        const string xslPath   = "transform.xsl";
        const string pdfPath   = "output.pdf";

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

        // Create XmlLoadOptions with the XSLT file.
        // This tells Aspose.Pdf to apply the stylesheet during loading.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML (with XSLT applied) into a PDF document.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}