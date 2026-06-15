using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddPageStampBackgroundExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with two pages
            using (Document sampleDoc = new Document())
            {
                // First page – main content
                Page firstPage = sampleDoc.Pages.Add();
                TextFragment tf1 = new TextFragment("This is the main page content.");
                firstPage.Paragraphs.Add(tf1);

                // Second page – stamp content
                Page stampPage = sampleDoc.Pages.Add();
                TextFragment tf2 = new TextFragment("STAMP");
                tf2.TextState.FontSize = 72;
                tf2.TextState.FontStyle = FontStyles.Bold;
                tf2.TextState.ForegroundColor = Color.Gray;
                stampPage.Paragraphs.Add(tf2);

                sampleDoc.Save("input.pdf");
            }

            // Open the PDF and add the stamp as background
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Create a stamp from the second page
                PdfPageStamp pageStamp = new PdfPageStamp(pdfDoc.Pages[2]);
                pageStamp.Background = true; // place stamp behind page content
                pageStamp.Opacity = 0.3f; // optional: make stamp semi‑transparent

                // Apply the stamp to the first page
                pdfDoc.Pages[1].AddStamp(pageStamp);

                pdfDoc.Save("output.pdf");
            }
        }
    }
}