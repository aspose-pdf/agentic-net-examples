using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a simple 1x1 pixel PNG image for stamping
        string imagePath = "stamp.png";
        byte[] pngBytes = new byte[] {
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
            0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
            0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
            0x08, 0x02, 0x00, 0x00, 0x00, 0x90, 0x77, 0x53,
            0xDE, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41,
            0x54, 0x08, 0xD7, 0x63, 0xF8, 0xCF, 0xC0, 0x00,
            0x00, 0x04, 0x00, 0x01, 0xE2, 0x26, 0x05, 0x9B,
            0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44,
            0xAE, 0x42, 0x60, 0x82
        };
        File.WriteAllBytes(imagePath, pngBytes);

        // Create a sample PDF document (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the PDF, add an image stamp, and optionally convert to PDF/A‑1b
        using (Document doc = new Document("input.pdf"))
        {
            ImageStamp stamp = new ImageStamp(imagePath);

            // Flag indicating whether PDF/A‑1b compliance is required
            bool generatePdfA1b = true;

            // Add alternative text only when PDF/A‑1b output is requested
            if (generatePdfA1b)
            {
                stamp.AlternativeText = "Sample logo";
            }

            // Add the stamp to the first page (page indexing is 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Convert to PDF/A‑1b if required
            if (generatePdfA1b)
            {
                string conversionLog = "conversion_log.xml";
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            }

            // Save the final document
            doc.Save("output.pdf");
        }
    }
}
