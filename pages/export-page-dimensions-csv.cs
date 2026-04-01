using System;
using System.IO;
using Aspose.Pdf;

public class ExportPageDimensions
{
    public static void Main()
    {
        // Create a sample PDF with two pages
        using (Document sampleDoc = new Document())
        {
            // Add first page
            sampleDoc.Pages.Add();
            // Add second page
            sampleDoc.Pages.Add();
            // Save the sample PDF
            sampleDoc.Save("sample.pdf");
        }

        // Open the sample PDF and export page dimensions to CSV
        using (Document pdfDoc = new Document("sample.pdf"))
        {
            using (StreamWriter writer = new StreamWriter("pages_dimensions.csv"))
            {
                writer.WriteLine("PageNumber,Width,Height");
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    // PageInfo.Width and Height are double; cast to float for consistency with the rest of the code
                    float width = (float)page.PageInfo.Width;
                    float height = (float)page.PageInfo.Height;
                    writer.WriteLine(string.Format("{0},{1},{2}", pageIndex, width, height));
                }
            }
        }
    }
}
