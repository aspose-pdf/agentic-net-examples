using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ScreenAnnotation lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_gif.pdf";
        const string gifPath = "animation.gif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"GIF file not found: {gifPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least three pages
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The PDF must contain at least three pages.");
                    return;
                }

                // Page indexing in Aspose.Pdf is 1‑based
                Page page = doc.Pages[3];

                // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
                Rectangle rect = new Rectangle(100, 500, 300, 700);

                // Create the ScreenAnnotation – the third argument is the media file path (GIF in this case)
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, gifPath)
                {
                    Name = "AnimatedGifAnnotation"
                };

                // The ScreenAnnotation constructor already embeds the media file.
                // Aspose.Pdf does not expose a separate RichMediaSettings class; the GIF will play
                // automatically when the page is opened (default activation mode).
                // Looping of GIFs is handled by the GIF format itself – most viewers will repeat
                // the animation indefinitely. No additional API is required.

                // Add the annotation to the page
                page.Annotations.Add(screenAnn);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"ScreenAnnotation with animated GIF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
