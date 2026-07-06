using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSLT stylesheet and the output PDF.
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

        // Load the XSLT file into XmlLoadOptions. This tells Aspose.Pdf to apply the
        // stylesheet while loading the XML, which resolves any namespace conflicts
        // defined in the XSLT (e.g., using distinct prefixes for each namespace).
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML document with the specified load options.
        // The constructor automatically applies the XSLT transformation.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}