using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_footer.pdf";  // result PDF
        const string footerImgPath = "footer.png";      // decorative footer image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerImgPath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImgPath}");
            return;
        }

        // Load the PDF document (using rule: create/load with using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Ensure a Footer object exists for the page
                if (page.Footer == null)
                    page.Footer = new HeaderFooter();

                // Create an Image object for the footer
                Image footerImage = new Image
                {
                    File = footerImgPath,
                    // Scale the image proportionally to 80% of the page width
                    FixWidth = page.PageInfo.Width * 0.8,
                    FixHeight = 0,                     // 0 preserves aspect ratio
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Add the image to the footer's paragraph collection
                page.Footer.Paragraphs.Add(footerImage);
            }

            // Save the modified PDF (using rule: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer image added to each page. Saved as '{outputPath}'.");
    }
}