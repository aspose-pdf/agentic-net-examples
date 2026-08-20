using System;
using System.IO;
using Aspose.Pdf; // XmlLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSLT stylesheet, and the output PDF.
        const string xmlPath = "input.xml";
        const string xslPath = "transform.xslt";
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

        // Load the XML and apply the XSLT during loading using XmlLoadOptions.
        // The constructor XmlLoadOptions(string xslFile) attaches the stylesheet.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Create the PDF document from the XML + XSLT.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}