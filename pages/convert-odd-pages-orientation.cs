using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with mixed orientation pages
        using (Document doc = new Document())
        {
            // Add four pages
            for (int i = 1; i <= 4; i++)
            {
                Page page = doc.Pages.Add();
                // Landscape for odd pages, portrait for even pages
                if (i % 2 == 1)
                {
                    // A4 landscape size (width > height)
                    page.SetPageSize(842.0, 595.0);
                    page.PageInfo.IsLandscape = true;
                }
                else
                {
                    // A4 portrait size
                    page.SetPageSize(595.0, 842.0);
                    page.PageInfo.IsLandscape = false;
                }
            }
            doc.Save("input.pdf");
        }

        // Load the PDF and convert odd‑numbered pages from landscape to portrait
        using (Document doc = new Document("input.pdf"))
        {
            int pageCount = doc.Pages.Count;
            for (int i = 1; i <= pageCount; i += 2) // odd pages (1‑based indexing)
            {
                Page page = doc.Pages[i];
                if (page.PageInfo.IsLandscape)
                {
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;
                    // Swap width and height to change orientation to portrait
                    page.SetPageSize(height, width);
                    page.PageInfo.IsLandscape = false;
                }
            }
            doc.Save("output.pdf");
        }
    }
}