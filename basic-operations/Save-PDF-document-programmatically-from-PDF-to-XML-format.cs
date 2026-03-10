using System;
using System.IO;
using Aspose.Pdf; // Document, XmlSaveOptions

class Program
{
    static void Main()
    {
        // Directory containing the source PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfFile))
        {
            // Initialize save options for XML export.
            XmlSaveOptions saveOptions = new XmlSaveOptions();

            // Save the document as XML using the options.
            pdfDocument.Save(xmlFile, saveOptions);
        }

        Console.WriteLine($"PDF successfully saved as XML to '{xmlFile}'.");
    }
}