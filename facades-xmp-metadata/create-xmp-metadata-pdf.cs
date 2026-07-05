using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "newpdf_with_xmp.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single blank page (required for a valid PDF)
            doc.Pages.Add();

            // Initialize the XMP metadata facade with the created document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Register a namespace prefix for Dublin Core metadata
                xmp.RegisterNamespaceURI("dc", "http://purl.org/dc/elements/1.1/");

                // Add custom XMP entries
                xmp.Add("dc:title", "Sample PDF with XMP");
                xmp.Add("dc:creator", "Aspose.Pdf Example");
                xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("o"));

                // Save the PDF together with the newly created XMP block
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}