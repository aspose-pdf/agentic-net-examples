using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for TextFragment

class Program
{
    static void Main()
    {
        // Paths
        const string outputPath = "output_with_timestamp.pdf";

        // Create a new PDF document and add a simple page with visible text
        using (Document doc = new Document())
        {
            // Add a blank page
            doc.Pages.Add();

            // Add a visible text fragment to demonstrate that the PDF is not empty
            TextFragment tf = new TextFragment("Sample PDF generated with XMP timestamp.");
            // Optional: set some basic text properties
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            doc.Pages[1].Paragraphs.Add(tf);

            // Create XMP metadata and bind it to the document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Register a custom namespace (optional, can use standard XMP namespaces)
                xmp.RegisterNamespaceURI("my", "http://example.com/ns");

                // Add a standard XMP creation date (ISO 8601 format)
                string timestamp = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-15T12:34:56.789Z
                xmp.Add("xmp:CreateDate", timestamp);

                // Add a custom property under the custom namespace (optional)
                xmp.Add("my:Timestamp", timestamp);

                // Save the PDF with the attached XMP metadata
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with XMP timestamp.");
    }
}