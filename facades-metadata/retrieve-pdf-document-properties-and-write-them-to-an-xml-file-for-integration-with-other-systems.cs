using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "pdf_properties.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load PDF metadata using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            // Retrieve desired properties
            string author      = pdfInfo.Author;
            string title       = pdfInfo.Title;
            string subject     = pdfInfo.Subject;
            string keywords    = pdfInfo.Keywords;
            string creator     = pdfInfo.Creator;
            string producer    = pdfInfo.Producer;
            // CreationDate and ModDate are strings in PdfFileInfo, not DateTime?
            string creation    = pdfInfo.CreationDate;
            string modDate     = pdfInfo.ModDate;
            int    pageCount   = pdfInfo.NumberOfPages;
            bool   isEncrypted = pdfInfo.IsEncrypted;
            bool   hasOpenPwd  = pdfInfo.HasOpenPassword;
            bool   hasEditPwd  = pdfInfo.HasEditPassword;
            bool   isPdfFile   = pdfInfo.IsPdfFile;
            bool   hasCollection = pdfInfo.HasCollection;

            // Build XML document with the extracted metadata
            XDocument xmlDoc = new XDocument(
                new XElement("PdfProperties",
                    new XElement("Author",      author ?? string.Empty),
                    new XElement("Title",       title ?? string.Empty),
                    new XElement("Subject",     subject ?? string.Empty),
                    new XElement("Keywords",    keywords ?? string.Empty),
                    new XElement("Creator",     creator ?? string.Empty),
                    new XElement("Producer",    producer ?? string.Empty),
                    new XElement("CreationDate", creation ?? string.Empty),
                    new XElement("ModDate",      modDate ?? string.Empty),
                    new XElement("NumberOfPages", pageCount),
                    new XElement("IsEncrypted",   isEncrypted),
                    new XElement("HasOpenPassword", hasOpenPwd),
                    new XElement("HasEditPassword", hasEditPwd),
                    new XElement("IsPdfFile",     isPdfFile),
                    new XElement("HasCollection", hasCollection)
                )
            );

            // Save the XML to the specified file
            xmlDoc.Save(outputXmlPath);
            Console.WriteLine($"PDF properties saved to '{outputXmlPath}'.");
        }
    }
}
