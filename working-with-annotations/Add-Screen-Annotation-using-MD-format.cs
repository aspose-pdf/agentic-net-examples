using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_screen.pdf"; // result PDF
        const string mediaPath  = "sample.mp4";         // media file to play

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation with the page, rectangle, and media file path
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, mediaPath)
            {
                Title = "Sample Video",          // optional title
                Contents = "Click to play video" // optional tooltip text
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screenAnn);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPath}'.");
    }
}