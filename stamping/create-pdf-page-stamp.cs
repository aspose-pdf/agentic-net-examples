using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace CreatePdfPageStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a template PDF with a single page containing sample text
            using (Document templateDoc = new Document())
            {
                Page templatePage = templateDoc.Pages.Add();
                TextFragment templateText = new TextFragment("Template Page");
                templatePage.Paragraphs.Add(templateText);
                templateDoc.Save("template.pdf");
            }

            // Create a target PDF where the stamp will be applied
            using (Document targetDoc = new Document())
            {
                Page targetPage = targetDoc.Pages.Add();
                TextFragment targetText = new TextFragment("Target Page");
                targetPage.Paragraphs.Add(targetText);
                targetDoc.Save("target.pdf");
            }

            // Load the template page to be used as a stamp
            using (Document stampSource = new Document("template.pdf"))
            {
                Page sourcePage = stampSource.Pages[1]; // 1‑based indexing

                // Create a PdfPageStamp from the source page
                PdfPageStamp pageStamp = new PdfPageStamp(sourcePage);
                pageStamp.Opacity = 0.5f; // semi‑transparent
                pageStamp.Background = true; // place behind content

                // Load the target PDF and apply the stamp
                using (Document target = new Document("target.pdf"))
                {
                    Page firstTargetPage = target.Pages[1];
                    firstTargetPage.AddStamp(pageStamp);
                    target.Save("output.pdf");
                }
            }
        }
    }
}