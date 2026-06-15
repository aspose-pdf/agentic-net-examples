using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF (self‑contained)
        using (Document sampleDoc = new Document())
        {
            Page samplePage = sampleDoc.Pages.Add();
            TextFragment sampleTf = new TextFragment("Sample PDF content");
            sampleTf.Position = new Position(100, 700);
            samplePage.Paragraphs.Add(sampleTf);
            sampleDoc.Save("input.pdf");
        }

        // Load the PDF from a memory stream
        byte[] pdfBytes = File.ReadAllBytes("input.pdf");
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            using (Document doc = new Document(inputStream))
            {
                // Create multi‑line watermark text
                TextStamp watermark = new TextStamp("Confidential\nDo Not Distribute");
                watermark.TextState.Font = FontRepository.FindFont("Helvetica");
                watermark.TextState.FontSize = 48;
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                watermark.Opacity = 0.5f;
                // In newer Aspose.Pdf versions the property is named Background; use it if available.
                // If the version provides IsBackground, the line below can be changed accordingly.
                watermark.Background = true;
                // Center the watermark on the page
                watermark.HorizontalAlignment = HorizontalAlignment.Center;
                watermark.VerticalAlignment = VerticalAlignment.Center;

                // Apply the watermark to each page (1‑based indexing)
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    Page currentPage = doc.Pages[i];
                    currentPage.AddStamp(watermark);
                }

                // Save the watermarked PDF
                doc.Save("output.pdf");
            }
        }
    }
}
