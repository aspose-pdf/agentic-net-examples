using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, XmlLoadOptions, XslFoLoadOptions)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xmlFile          = Path.Combine(dataDir, "input.xml");      // XML source
        string pdfFile          = Path.Combine(dataDir, "output.pdf");    // PDF destination
        string imagesDirectory  = Path.Combine(dataDir, "Images");        // Directory containing images referenced in the XML

        // Verify that required files/directories exist
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }
        if (!Directory.Exists(imagesDirectory))
        {
            Console.Error.WriteLine($"Images directory not found: {imagesDirectory}");
            return;
        }

        // Use XslFoLoadOptions (derived from XmlLoadOptions) to specify the base path for image resources.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        loadOptions.BasePath = imagesDirectory;   // <-- image directory specification

        // Load the XML document with the configured options and convert it to PDF.
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions needed because the target format is PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"XML successfully converted to PDF: {pdfFile}");
    }
}