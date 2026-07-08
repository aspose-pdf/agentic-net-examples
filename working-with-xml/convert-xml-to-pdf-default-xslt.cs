using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the XML source file
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input XML file path
        string xmlPath = Path.Combine(dataDir, "input.xml");

        // Desired output PDF file path
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load options for XML without providing an XSL file (default transformation)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Load the XML and convert it to a PDF document
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the generated PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at: {pdfPath}");
    }
}