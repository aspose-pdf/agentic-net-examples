using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source files.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the XML file to be converted.
        string xmlFile = Path.Combine(dataDir, "input.xml");

        // Path to the XSL-FO (XSL) stylesheet that defines the transformation.
        string xslFile = Path.Combine(dataDir, "transform.xsl");

        // Desired output PDF file path.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the input files exist.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        if (!File.Exists(xslFile))
        {
            Console.Error.WriteLine($"XSL file not found: {xslFile}");
            return;
        }

        // Initialize XmlLoadOptions with the XSL stylesheet.
        // The constructor XmlLoadOptions(string xslFile) sets the XSL data used for the transformation.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFile);

        // Load the XML file, applying the XSL transformation, and create a PDF document.
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"XML successfully converted to PDF: {pdfFile}");
    }
}