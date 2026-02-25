using System;
using System.IO;
using Aspose.Pdf;   // XmlSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file path.
        string pdfFile = Path.Combine(dataDir, "PDF-to-XML.pdf");

        // Desired output XML file path.
        string xmlFile = Path.Combine(dataDir, "PDF-to-XML.xml");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfFile}");
            return;
        }

        // Load the PDF and save it as XML using XmlSaveOptions.
        using (Document pdfDocument = new Document(pdfFile))
        {
            // Initialize the XML save options (required for non‑PDF output).
            XmlSaveOptions saveOptions = new XmlSaveOptions();

            // Perform the conversion.
            pdfDocument.Save(xmlFile, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XML: {xmlFile}");
    }
}