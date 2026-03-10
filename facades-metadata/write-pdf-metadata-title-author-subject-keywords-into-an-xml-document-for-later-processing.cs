using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade for accessing PDF metadata
using System.Xml.Linq;            // Simple XML creation

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Source PDF file
        const string xmlPath = "metadata.xml";   // Output XML file

        // Verify that the PDF exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using the PdfFileInfo facade (wrapped in using for deterministic disposal)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the required metadata fields; fallback to empty string if null
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Construct a simple XML document containing the metadata
            XDocument metadataXml = new XDocument(
                new XElement("PdfMetadata",
                    new XElement("Title",    title),
                    new XElement("Author",   author),
                    new XElement("Subject",  subject),
                    new XElement("Keywords", keywords)
                )
            );

            // Save the XML document to the specified path
            metadataXml.Save(xmlPath);
        }

        Console.WriteLine($"Metadata extracted and saved to '{xmlPath}'.");
    }
}