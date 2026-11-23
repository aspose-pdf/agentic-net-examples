using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";      // optional existing PDF
        const string outputPath  = "output.pdf";
        const string bgImagePath = "background.jpg"; // background image file

        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load existing PDF if present; otherwise create a new document with one page
        Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document();
        using (doc)
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Add background image to the first page
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            ImageStamp bgStamp = new ImageStamp(bgImagePath)
            {
                Background = true,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Width  = firstPage.PageInfo.Width,
                Height = firstPage.PageInfo.Height
            };
            firstPage.AddStamp(bgStamp);

            // Add a blank page without any background
            doc.Pages.Add();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}