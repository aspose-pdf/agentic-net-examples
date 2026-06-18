using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddTextStampAllPages
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with three pages
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Pages.Add();
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the PDF and add a text stamp to each page
            using (Document pdfDoc = new Document("input.pdf"))
            {
                TextStamp stamp = new TextStamp("CONFIDENTIAL");
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Center;
                stamp.Opacity = 0.5f;
                stamp.TextState.FontSize = 48;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.Font = FontRepository.FindFont("Arial");

                int pageCount = pdfDoc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    pdfDoc.Pages[pageIndex].AddStamp(stamp);
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}