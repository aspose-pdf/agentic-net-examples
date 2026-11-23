using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string backgroundImagePath = "background.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least six pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain six pages.");
                return;
            }

            // Get the last page (pages are 1‑based)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Create an ImageStamp and set it as a background image
            ImageStamp bgStamp = new ImageStamp(backgroundImagePath)
            {
                Background = true, // place behind existing page content
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = lastPage.PageInfo.Width,
                Height = lastPage.PageInfo.Height
            };

            // Add the background stamp to the last page
            lastPage.AddStamp(bgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background image added to last page and saved as '{outputPath}'.");
    }
}