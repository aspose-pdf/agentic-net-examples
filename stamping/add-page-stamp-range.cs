using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with 4 pages (evaluation limit)
        using (Document doc = new Document())
        {
            for (int i = 0; i < 4; i++)
            {
                doc.Pages.Add();
            }
            doc.Save("input.pdf");
        }

        // Open the PDF and add stamps to pages 5 through 10
        using (Document pdfDoc = new Document("input.pdf"))
        {
            for (int pageNumber = 5; pageNumber <= 10; pageNumber++)
            {
                if (pageNumber <= pdfDoc.Pages.Count)
                {
                    Page page = pdfDoc.Pages[pageNumber];
                    PageNumberStamp stamp = new PageNumberStamp("Page #");
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment = VerticalAlignment.Bottom;
                    stamp.BottomMargin = 10f;
                    page.AddStamp(stamp);
                }
            }
            pdfDoc.Save("output.pdf");
        }
    }
}
