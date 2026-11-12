using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "newDocument.pdf";

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a single blank page.
            doc.Pages.Add();

            // Build a fresh XMP metadata block using the PdfXmpMetadata facade.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Register common namespaces (optional but recommended).
                xmp.RegisterNamespaceURI("dc", "http://purl.org/dc/elements/1.1/");
                xmp.RegisterNamespaceURI("xmp", "http://ns.adobe.com/xap/1.0/");
                xmp.RegisterNamespaceURI("pdf", "http://ns.adobe.com/pdf/1.3/");

                // Add XMP properties.
                xmp.Add("dc:creator", "John Doe");
                xmp.Add("dc:title", "Sample PDF with XMP");
                xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                xmp.Add("xmp:ModifyDate", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                xmp.Add("pdf:Producer", "Aspose.Pdf for .NET");

                // Save the PDF together with the XMP metadata.
                xmp.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF with XMP metadata saved to '{pdfPath}'.");
    }
}
