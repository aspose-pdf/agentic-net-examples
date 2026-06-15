using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddFullPageWatermarkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF that will receive the watermark
            using (Document sourceDoc = new Document())
            {
                Page sourcePage = sourceDoc.Pages.Add();
                TextFragment sourceText = new TextFragment("Hello World!");
                sourcePage.Paragraphs.Add(sourceText);
                sourceDoc.Save("input.pdf");
            }

            // Step 2: Create a PDF that contains the watermark content (a single page)
            using (Document stampDoc = new Document())
            {
                Page stampPage = stampDoc.Pages.Add();
                TextFragment watermarkText = new TextFragment("CONFIDENTIAL");
                watermarkText.TextState.FontSize = 72;
                watermarkText.TextState.FontStyle = FontStyles.Bold;
                watermarkText.TextState.ForegroundColor = Color.Red;
                watermarkText.TextState.Font = FontRepository.FindFont("Arial");
                watermarkText.Position = new Position(100, 400);
                stampPage.Paragraphs.Add(watermarkText);
                stampDoc.Save("stamp.pdf");
            }

            // Step 3: Load the target PDF and apply the page stamp as a background watermark
            using (Document targetDoc = new Document("input.pdf"))
            {
                using (Document stampSource = new Document("stamp.pdf"))
                {
                    Page stampSourcePage = stampSource.Pages[1];
                    PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage);
                    pageStamp.Background = true; // place behind page content
                    pageStamp.Opacity = 0.3f; // semi‑transparent
                    // Make the stamp cover the whole page
                    pageStamp.Width = targetDoc.Pages[1].PageInfo.Width;
                    pageStamp.Height = targetDoc.Pages[1].PageInfo.Height;
                    pageStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    pageStamp.VerticalAlignment = VerticalAlignment.Center;

                    foreach (Page page in targetDoc.Pages)
                    {
                        page.AddStamp(pageStamp);
                    }
                }

                targetDoc.Save("output.pdf");
            }
        }
    }
}
