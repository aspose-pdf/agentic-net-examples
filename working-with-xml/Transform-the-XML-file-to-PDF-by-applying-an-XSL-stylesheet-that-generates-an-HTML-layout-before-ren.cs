using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xmlPath         = Path.Combine(dataDir, "source.xml");   // input XML
        string xslPath         = Path.Combine(dataDir, "layout.xsl");   // XSL that creates HTML layout
        string pdfOutputPath   = Path.Combine(dataDir, "result.pdf");   // output PDF

        // Verify files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL file not found: {xslPath}");
            return;
        }

        // Load XML with the XSL stylesheet – Aspose.Pdf will apply the XSL (producing HTML) and render to PDF
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Document constructor loads the XML using the provided options
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF generated successfully at: {pdfOutputPath}");
    }
}