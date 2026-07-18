using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_timestamp.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page so the PDF is not empty
            doc.Pages.Add();

            // Initialize the XMP metadata facade for this document
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Add a custom timestamp property (ISO 8601 format) to the XMP metadata
            string timestamp = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-15T12:34:56.789Z
            xmp.Add("xmp:CreateDate", timestamp);

            // Save the PDF; the XMP metadata is embedded automatically
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with timestamp metadata to '{outputPath}'.");
    }
}