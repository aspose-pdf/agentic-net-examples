using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace RotateTextStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Open the PDF and add a rotated text stamp to each page
            using (Document pdfDoc = new Document("input.pdf"))
            {
                int pageCount = pdfDoc.Pages.Count;
                // Evaluation mode limits collections to 4 elements; iterate safely
                for (int i = 1; i <= pageCount && i <= 4; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    TextStamp stamp = new TextStamp("CONFIDENTIAL");
                    stamp.RotateAngle = 30; // Rotate 30 degrees for slanted effect
                    stamp.Opacity = 0.5; // Semi‑transparent
                    stamp.Background = true; // Place behind page content
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment = VerticalAlignment.Center;
                    page.AddStamp(stamp);
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}