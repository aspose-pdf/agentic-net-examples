using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, XSLT stylesheet and the output PDF.
        string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xmlPath   = Path.Combine(dataDir, "source.xml");
        string xslPath   = Path.Combine(dataDir, "transform.xslt");
        string pdfPath   = Path.Combine(dataDir, "output.pdf");

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

        // Initialize load options with the XSLT stylesheet.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML and apply the XSLT to generate a PDF document.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at: {pdfPath}");
    }
}