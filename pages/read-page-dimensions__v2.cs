using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with two pages of different sizes
        using (Document sampleDoc = new Document())
        {
            // First page uses default size
            sampleDoc.Pages.Add();

            // Second page with custom size
            Aspose.Pdf.Page secondPage = sampleDoc.Pages.Add();
            secondPage.SetPageSize(500, 700); // width, height in points

            sampleDoc.Save("sample.pdf");
        }

        // Open the PDF and log page dimensions
        using (Document doc = new Document("sample.pdf"))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine("Document contains " + pageCount + " pages.");

            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIndex];
                Aspose.Pdf.Rectangle rect = page.Rect;
                Console.WriteLine("Page " + pageIndex + ": Width = " + rect.Width + ", Height = " + rect.Height);
            }
        }
    }
}