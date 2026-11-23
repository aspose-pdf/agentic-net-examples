using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string bgImagePath1   = "background1.jpg";
        const string bgImagePath2   = "background2.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bgImagePath1) || !File.Exists(bgImagePath2))
        {
            Console.Error.WriteLine("One or both background images not found.");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // === Add background image to the first page ===
            // Page indexing in Aspose.Pdf is 1‑based.
            Page page1 = doc.Pages[1];
            ImageStamp bgStamp1 = new ImageStamp(bgImagePath1)
            {
                // Mark as background so it appears behind existing content.
                Background = true,
                // Stretch to fill the whole page.
                Width  = page1.PageInfo.Width,
                Height = page1.PageInfo.Height,
                // Align to the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };
            page1.AddStamp(bgStamp1);

            // === Add background image to the second page ===
            if (doc.Pages.Count >= 2)
            {
                Page page2 = doc.Pages[2];
                ImageStamp bgStamp2 = new ImageStamp(bgImagePath2)
                {
                    Background = true,
                    Width  = page2.PageInfo.Width,
                    Height = page2.PageInfo.Height,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };
                page2.AddStamp(bgStamp2);
            }
            else
            {
                Console.WriteLine("PDF has only one page; second background image not applied.");
            }

            // Save the modified PDF. No SaveOptions needed because we are saving to PDF format.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with background images to '{outputPdfPath}'.");
    }
}