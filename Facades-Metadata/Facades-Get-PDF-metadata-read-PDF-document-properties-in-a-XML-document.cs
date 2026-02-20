using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path and output XML path
        const string pdfPath = "input.pdf";
        const string xmlPath = "metadata.xml";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load PDF metadata using the PdfFileInfo facade
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Build an XML document with the extracted metadata
            XDocument metadataXml = new XDocument(
                new XElement("PdfMetadata",
                    new XElement("Title", pdfInfo.Title ?? string.Empty),
                    new XElement("Author", pdfInfo.Author ?? string.Empty),
                    new XElement("Subject", pdfInfo.Subject ?? string.Empty),
                    new XElement("Keywords", pdfInfo.Keywords ?? string.Empty),
                    new XElement("Creator", pdfInfo.Creator ?? string.Empty),
                    new XElement("Producer", pdfInfo.Producer ?? string.Empty),
                    // CreationDate and ModDate are already strings, no formatting needed
                    new XElement("CreationDate", pdfInfo.CreationDate ?? string.Empty),
                    new XElement("ModDate", pdfInfo.ModDate ?? string.Empty),
                    new XElement("NumberOfPages", pdfInfo.NumberOfPages),
                    new XElement("IsEncrypted", pdfInfo.IsEncrypted),
                    new XElement("IsPdfFile", pdfInfo.IsPdfFile),
                    new XElement("HasOpenPassword", pdfInfo.HasOpenPassword),
                    new XElement("HasEditPassword", pdfInfo.HasEditPassword),
                    // Header is a Dictionary<string,string>; convert it to a readable string
                    new XElement("Header", pdfInfo.Header != null
                        ? string.Join("; ", pdfInfo.Header.Select(kv => $"{kv.Key}={kv.Value}"))
                        : string.Empty)
                )
            );

            // Save the XML document to the specified file
            metadataXml.Save(xmlPath);

            Console.WriteLine($"Metadata extracted and saved to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
