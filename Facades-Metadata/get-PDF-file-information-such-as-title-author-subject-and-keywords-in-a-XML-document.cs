using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileInfo resides here
using System.Xml.Linq;            // For building XML

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Source PDF file
        const string outputXml = "info.xml";    // Destination XML file

        // Verify the PDF exists before processing
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable – use a using block for deterministic cleanup
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Retrieve metadata; null values are replaced with empty strings
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Construct an XML document with the extracted information
            XDocument xmlDoc = new XDocument(
                new XElement("PDFInfo",
                    new XElement("Title",    title),
                    new XElement("Author",   author),
                    new XElement("Subject",  subject),
                    new XElement("Keywords", keywords)
                )
            );

            // Save the XML to the specified path
            xmlDoc.Save(outputXml);
        }

        Console.WriteLine($"Metadata saved to '{outputXml}'.");
    }
}