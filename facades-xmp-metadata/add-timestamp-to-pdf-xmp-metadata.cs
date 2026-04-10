using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "generated_with_timestamp.pdf";

        // Create a new PDF document (lifecycle: create)
        using (Document doc = new Document())
        {
            // Add a page and a simple text fragment
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF generated with timestamp metadata."));

            // Initialize the XMP metadata facade on the document (lifecycle: create)
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Prepare an ISO‑8601 timestamp
                string timestamp = DateTime.UtcNow.ToString("o"); // e.g., 2023-09-15T12:34:56.789Z

                // Add the timestamp to a standard XMP property (CreateDate)
                xmp.Add("xmp:CreateDate", timestamp);

                // Optionally also set the MetadataDate property
                xmp.Add("xmp:MetadataDate", timestamp);

                // Save the PDF with the updated XMP metadata (lifecycle: save)
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with timestamp XMP metadata.");
    }
}