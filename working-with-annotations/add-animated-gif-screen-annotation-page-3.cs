using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string gifPath    = "animation.gif";

        // Verify required files exist
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

        // Load the PDF (using rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (page indexing is 1‑based)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Define the annotation rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation on page 3 that references the animated GIF
            // Constructor: ScreenAnnotation(Page page, Rectangle rect, string mediaFile)
            ScreenAnnotation screen = new ScreenAnnotation(doc.Pages[3], rect, gifPath);

            // Add the annotation to the page's annotation collection
            doc.Pages[3].Annotations.Add(screen);

            // Save the modified PDF (Document.Save writes PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}