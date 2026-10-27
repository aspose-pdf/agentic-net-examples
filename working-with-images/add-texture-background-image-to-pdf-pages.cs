using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string backgroundImagePath = "texture.png";

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

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp from the texture file
                ImageStamp stamp = new ImageStamp(backgroundImagePath)
                {
                    // Place the stamp behind existing page content
                    Background = true,
                    // Reduce opacity so the texture is subtle
                    Opacity = 0.3f,
                    // Stretch the image to cover the whole page
                    Width = page.PageInfo.Width,
                    Height = page.PageInfo.Height,
                    // Align to the lower‑left corner (default XIndent/YIndent = 0)
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    XIndent = 0,
                    YIndent = 0
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background texture applied and saved to '{outputPath}'.");
    }
}
