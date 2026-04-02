using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with two pages of different widths
        using (Document sampleDoc = new Document())
        {
            // First page - wide (width > 600 points)
            Page widePage = sampleDoc.Pages.Add();
            widePage.PageInfo.Width = 700;
            widePage.PageInfo.Height = 500;

            // Second page - narrow (width <= 600 points)
            Page narrowPage = sampleDoc.Pages.Add();
            narrowPage.PageInfo.Width = 500;
            narrowPage.PageInfo.Height = 500;

            sampleDoc.Save("input.pdf");
        }

        // Load the PDF and resize only wide pages to A4 size
        using (Document doc = new Document("input.pdf"))
        {
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                if (page.PageInfo.Width > 600)
                {
                    page.Resize(PageSize.A4);
                }
            }

            doc.Save("output.pdf");
        }
    }
}