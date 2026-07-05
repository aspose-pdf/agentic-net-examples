using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, XSLT stylesheet and the output PDF.
        const string xmlPath   = "input.xml";
        const string xslPath   = "transform.xslt";
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

        // Load the XML and apply the XSLT during conversion.
        // XmlLoadOptions(string) accepts the XSLT file to be used.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}