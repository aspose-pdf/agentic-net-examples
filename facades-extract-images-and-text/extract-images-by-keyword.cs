using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string keyword = "YOUR_KEYWORD"; // keyword to search for
        const string imagesDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(imagesDir);

        // 1. Determine which pages contain the keyword
        List<int> pagesWithKeyword = new List<int>();

        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                // Extract text only from the current page
                doc.Pages[i].Accept(absorber);
                if (!string.IsNullOrEmpty(absorber.Text) &&
                    absorber.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    pagesWithKeyword.Add(i);
                }
            }
        }

        if (pagesWithKeyword.Count == 0)
        {
            Console.WriteLine("No pages contain the specified keyword.");
            return;
        }

        // 2. Extract images only from the identified pages
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: define the extraction mode (resource‑based images)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            int imageIndex = 1;
            foreach (int pageNumber in pagesWithKeyword)
            {
                // Limit extraction to a single page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Perform the extraction for the current page
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(imagesDir, $"Image_Page{pageNumber}_{imageIndex}.png");
                    extractor.GetNextImage(imagePath, ImageFormat.Png);
                    Console.WriteLine($"Saved image: {imagePath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
