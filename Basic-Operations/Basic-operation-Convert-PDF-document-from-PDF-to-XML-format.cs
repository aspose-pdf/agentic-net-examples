using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the directory that contains the input PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfFile = Path.Combine(dataDir, "PDF-to-XML.pdf");

        // Desired output XML file.
        string xmlFile = Path.Combine(dataDir, "PDF-to-XML.xml");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfFile}'.");
            return;
        }

        // Load the PDF document (create/load rule).
        using (Document pdfDocument = new Document(pdfFile))
        {
            // Save the document as XML (save rule).
            pdfDocument.Save(xmlFile);
        }

        Console.WriteLine($"PDF successfully converted to XML: {xmlFile}");
    }
}