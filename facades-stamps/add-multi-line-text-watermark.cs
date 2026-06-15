using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddMultiLineTextWatermark
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------
            // 1. Create a sample PDF (self‑contained example)
            // ------------------------------------------------------------
            using (Document sampleDoc = new Document())
            {
                // Add a single blank page – evaluation mode allows up to 4 pages
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // ------------------------------------------------------------
            // 2. Open the PDF and add a multi‑line text watermark
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Build the watermark text – each line separated by a newline character
                string watermark = "First line\nSecond line\nThird line";

                // Create a TextStamp (core API) – this class can be added directly to a Page
                TextStamp textStamp = new TextStamp(watermark);
                // Font settings – use the FontRepository to obtain a font object
                textStamp.TextState.Font = FontRepository.FindFont("Arial");
                textStamp.TextState.FontSize = 36;
                // Use Aspose.Pdf.Color (fully qualified) for the text colour
                textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                // Center the watermark on the page
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment   = VerticalAlignment.Center;
                // Make the watermark semi‑transparent
                textStamp.Opacity = 0.5f;

                // Apply the stamp to every page (1‑based indexing as required by the rules)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    page.AddStamp(textStamp);
                }

                // Save the watermarked PDF
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
