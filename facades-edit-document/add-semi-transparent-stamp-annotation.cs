using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Get page three (1‑based indexing)
            Page page = doc.Pages[3];

            // Define the annotation rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a stamp annotation on page three
            StampAnnotation stamp = new StampAnnotation(page, rect)
            {
                Contents = "Semi‑transparent annotation",
                Color = Aspose.Pdf.Color.Yellow,
                Opacity = 0.6 // 60% opacity
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(stamp);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with opacity 0.6 and saved to '{outputPath}'.");
    }
}