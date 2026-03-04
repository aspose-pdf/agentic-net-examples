using System;
using System.IO;
using Aspose.Pdf; // Document, XmlSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Define input PDF and output XML paths
        string dataDir = "YOUR_DATA_DIRECTORY";
        string pdfPath = Path.Combine(dataDir, "PDF-to-XML.pdf");
        string xmlPath = Path.Combine(dataDir, "PDF-to-XML.xml");

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize XmlSaveOptions (required to save to a non‑PDF format)
            XmlSaveOptions saveOptions = new XmlSaveOptions();

            // Save the document as XML using the options
            pdfDocument.Save(xmlPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully saved as XML to '{xmlPath}'.");
    }
}