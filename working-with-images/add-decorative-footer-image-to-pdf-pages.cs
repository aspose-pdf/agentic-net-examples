using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerImagePath = "footer.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine page width and a reasonable footer height (10% of page height)
                double pageWidth  = page.PageInfo.Width;
                double footerHeight = page.PageInfo.Height * 0.10;

                // Define a rectangle that spans the full page width at the bottom.
                // The rectangle's height is set to the desired footer height.
                // AddImage will keep the image's aspect ratio and center it within this rectangle.
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    0,                     // llx (left)
                    0,                     // lly (bottom)
                    pageWidth,             // urx (right)
                    footerHeight           // ury (top)
                );

                // Add the image to the page. autoAdjustRectangle = true (default) preserves proportions.
                page.AddImage(footerImagePath, footerRect);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with decorative footer saved to '{outputPath}'.");
    }
}