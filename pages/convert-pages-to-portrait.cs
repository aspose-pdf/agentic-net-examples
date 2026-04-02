using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a landscape page
        using (Document sampleDoc = new Document())
        {
            // Add a page
            Page page = sampleDoc.Pages.Add();

            // Set page size to A4 landscape (height > width swapped)
            page.SetPageSize(PageSize.A4.Height, PageSize.A4.Width);

            // Save the sample PDF
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and convert all pages to portrait orientation
        using (Document doc = new Document("input.pdf"))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double width = mediaBox.URX - mediaBox.LLX;   // use uppercase property names
                double height = mediaBox.URY - mediaBox.LLY;

                if (width > height)
                {
                    // Swap width and height to make portrait
                    page.SetPageSize(height, width);
                }
            }

            // Save the result
            doc.Save("output.pdf");
        }
    }
}