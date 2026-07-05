using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single blank page (required for a valid PDF)
            doc.Pages.Add();

            // Initialize the XMP metadata facade and bind it to the document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(doc);

                // Generate an ISO‑8601 UTC timestamp
                string timestamp = DateTime.UtcNow.ToString("o");

                // Add the timestamp as a custom XMP property (e.g., xmp:CreateDate)
                xmp.Add("xmp:CreateDate", timestamp);

                // Persist the changes together with the PDF content
                // PdfXmpMetadata.Save requires the output file path
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with timestamp XMP saved to '{outputPath}'.");
    }
}
