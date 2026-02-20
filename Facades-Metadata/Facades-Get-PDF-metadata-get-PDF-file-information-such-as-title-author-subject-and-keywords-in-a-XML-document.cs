using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output XML path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf> <output-xml>");
            return;
        }

        string pdfPath = args[0];
        string xmlPath = args[1];

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load PDF metadata using the Facade class PdfFileInfo
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Extract required metadata fields
            string title = pdfInfo.Title ?? string.Empty;
            string author = pdfInfo.Author ?? string.Empty;
            string subject = pdfInfo.Subject ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Build an XML document with the extracted information
            XDocument xmlDoc = new XDocument(
                new XElement("PdfMetadata",
                    new XElement("Title", title),
                    new XElement("Author", author),
                    new XElement("Subject", subject),
                    new XElement("Keywords", keywords)
                )
            );

            // Save the XML document to the specified output file
            xmlDoc.Save(xmlPath);

            Console.WriteLine($"Metadata extracted and saved to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}