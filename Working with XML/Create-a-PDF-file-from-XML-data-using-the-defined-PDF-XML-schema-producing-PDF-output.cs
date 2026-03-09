using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the XML file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input XML file (must follow the Aspose.Pdf XML schema).
        string xmlFile = Path.Combine(dataDir, "input.xml");

        // Desired output PDF file.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the XML source exists.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        // Load options for XML → PDF conversion (no XSL transformation required).
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Load the XML and create a PDF document.
        using (Document pdfDoc = new Document(xmlFile, loadOptions))
        {
            // Save the generated PDF.
            pdfDoc.Save(pdfFile);
        }

        Console.WriteLine($"PDF successfully created at: {pdfFile}");
    }
}