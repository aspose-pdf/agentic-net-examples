using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Path for the generated PDF
        const string outputPath = "generated_with_timestamp.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for a valid PDF)
            doc.Pages.Add();

            // OPTIONAL: add some content to the page
            // (demonstrates that the PDF is not empty)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(new TextFragment("Sample PDF generated with timestamp."));

            // Initialize XMP metadata facade bound to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Add a timestamp property (ModifyDate) in ISO 8601 format
            // Using the string key "xmp:ModifyDate" and a formatted DateTime value
            string timestamp = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-18T12:34:56.789Z
            xmp.Add("xmp:ModifyDate", timestamp);

            // Save the PDF (Document disposal handled by using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with XMP timestamp.");
    }
}