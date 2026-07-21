using System;
using System.IO;
using Aspose.Pdf;               // Document, PageCollectionExtensions, XmlLoadOptions

class Program
{
    static void Main()
    {
        const string xmlPath    = "input.xml";      // XML containing XSL‑FO with pagination placeholder
        const string outputPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML (XSL‑FO) into a PDF document using the proper load options
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Apply pagination artifacts defined in the XML to all pages
            doc.Pages.UpdatePagination();

            // Persist the PDF with automatic page numbers
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with automatic page numbers saved to '{outputPath}'.");
    }
}