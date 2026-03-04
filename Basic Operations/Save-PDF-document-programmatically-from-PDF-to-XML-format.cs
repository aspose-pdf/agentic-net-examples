using System;
using System.IO;
using Aspose.Pdf; // XmlSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the XML will be written
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file path
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Desired output XML file path
        string xmlPath = Path.Combine(dataDir, "output.xml");

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize save options for XML output
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML, passing the options explicitly
            pdfDocument.Save(xmlPath, xmlOptions);
        }

        Console.WriteLine($"PDF successfully saved as XML to '{xmlPath}'.");
    }
}