using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_gif.pdf";
        const string gifPath    = "animation.gif"; // animated GIF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"Animated GIF not found: {gifPath}");
            return;
        }

        // Load the PDF document (using the recommended using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document must contain at least three pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based; page 3 is the third page
            Page pageThree = doc.Pages[3];

            // Define the rectangle where the annotation will appear (coordinates in points)
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation that points to the animated GIF file
            ScreenAnnotation screen = new ScreenAnnotation(pageThree, rect, gifPath)
            {
                // Optional: set a title and contents for accessibility / tooltip
                Title    = "Animated GIF",
                Contents = "An animated GIF that loops continuously."
            };

            // The looping behavior for GIFs is handled by the viewer; no extra property is required.
            // Add the annotation to the page's annotation collection.
            pageThree.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: '{outputPath}'.");
    }
}