using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddDiagonalWatermark
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with two pages (evaluation mode limit)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Open the PDF and add a diagonal watermark to each page
            using (Document pdfDocument = new Document("input.pdf"))
            {
                int pageCount = pdfDocument.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = pdfDocument.Pages[i];
                    TextStamp stamp = new TextStamp("CONFIDENTIAL");
                    stamp.TextState.FontSize = 72;
                    stamp.TextState.FontStyle = FontStyles.Bold;
                    stamp.TextState.ForegroundColor = Color.Gray;
                    stamp.Opacity = 0.5f;
                    stamp.RotateAngle = 45;
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment = VerticalAlignment.Center;
                    stamp.Background = true;
                    page.AddStamp(stamp);
                }
                pdfDocument.Save("output.pdf");
            }
        }
    }
}