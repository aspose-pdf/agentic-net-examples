using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple PDF file (self‑contained example)
        using (Document createDoc = new Document())
        {
            // Add a blank page
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Step 2: Prepare a tiny PNG image (1x1 red pixel) as a memory stream
        string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
        byte[] pngBytes = Convert.FromBase64String(base64Png);
        using (MemoryStream imageStream = new MemoryStream(pngBytes))
        {
            // Step 3: Open the PDF, add image stamp and custom XMP metadata
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Create an image stamp from the stream
                ImageStamp stamp = new ImageStamp(imageStream);
                stamp.Opacity = 0.5f; // 50% transparent
                stamp.Background = false; // stamp on top of page content
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Center;
                stamp.Width = 100; // width in points
                stamp.Height = 100; // height in points

                // Apply the stamp to the first page (1‑based indexing)
                pdfDoc.Pages[1].AddStamp(stamp);

                // Register a custom namespace for XMP metadata
                pdfDoc.Metadata.RegisterNamespaceUri("custom", "http://example.com/custom");
                // Add custom XMP entries
                pdfDoc.Metadata["custom:Author"] = "John Doe";
                pdfDoc.Metadata["custom:Description"] = "Sample PDF with image stamp and retained metadata";

                // Save the modified PDF
                pdfDoc.Save("output.pdf");
            }
        }

        Console.WriteLine("PDF created with image stamp and custom XMP metadata.");
    }
}
