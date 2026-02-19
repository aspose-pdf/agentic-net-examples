using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // PdfXmlLoadOptions resides here

class Program
{
    static void Main()
    {
        // Define the directory that contains the source XML file.
        // Replace with an absolute path or keep relative to the executable.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        // Input XML file (PDF/A represented in XML format).
        string xmlFile = Path.Combine(dataDir, "source.pdfxml");

        // Desired output PDF file.
        string pdfFile = Path.Combine(dataDir, "converted.pdf");

        // Verify that the source XML exists.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"Error: XML source file not found at '{xmlFile}'.");
            return;
        }

        try
        {
            // Load the XML representation of the PDF/A document.
            PdfXmlLoadOptions loadOptions = new PdfXmlLoadOptions();
            using (Document pdfDocument = new Document(xmlFile, loadOptions))
            {
                // Save the document as a regular PDF.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"Conversion successful. PDF saved to '{pdfFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}