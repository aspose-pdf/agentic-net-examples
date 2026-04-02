using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the absolute path to the "Data" folder relative to the executable location.
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string dataDir = Path.Combine(basePath, "Data");

        // Build full paths for the XML input, XSLT stylesheet and the output PDF.
        string xmlFile = Path.Combine(dataDir, "input.xml");
        string xslFile = Path.Combine(dataDir, "transform.xsl");
        string outputPdf = Path.Combine(basePath, "output.pdf");

        // Verify that the required files exist – this gives a clear error message instead of a generic DirectoryNotFoundException.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML input file not found: {xmlFile}");
            return;
        }
        if (!File.Exists(xslFile))
        {
            Console.Error.WriteLine($"XSLT stylesheet not found: {xslFile}");
            return;
        }

        // Load options with the custom XSLT stylesheet.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFile);

        // Load XML, apply XSLT and convert to PDF.
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }
}
