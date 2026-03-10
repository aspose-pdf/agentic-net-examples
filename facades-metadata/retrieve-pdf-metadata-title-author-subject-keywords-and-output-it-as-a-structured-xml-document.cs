using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileInfo resides here
using System.Xml.Linq;            // For building XML

class Program
{
    static void Main()
    {
        // Input PDF file and output XML file paths
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "metadata.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load PDF metadata using the Facade class PdfFileInfo
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            // Retrieve required metadata properties
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Build a simple XML structure with the metadata
            XDocument xmlDoc = new XDocument(
                new XElement("Metadata",
                    new XElement("Title",    title),
                    new XElement("Author",   author),
                    new XElement("Subject",  subject),
                    new XElement("Keywords", keywords)
                )
            );

            // Save the XML document to the specified file
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Metadata extracted to '{outputXmlPath}'.");
    }
}