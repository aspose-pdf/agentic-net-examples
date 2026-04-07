using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_strikeout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that contains the paragraph (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle that covers the paragraph.
            // Adjust the coordinates (llx, lly, urx, ury) as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 520);

            // Create a strike‑out annotation on the specified page and rectangle
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect)
            {
                // Set the author of the annotation
                Title = "reviewer",
                // Optional: set the color of the strike line
                Color = Aspose.Pdf.Color.Red
            };

            // Add the annotation to the page
            page.Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strike‑out annotation added. Saved to '{outputPath}'.");
    }
}