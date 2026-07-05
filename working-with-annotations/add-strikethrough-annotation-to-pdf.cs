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
        const string reviewer   = "Reviewer";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that contains the paragraph (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that covers the paragraph.
            // Adjust the coordinates (llx, lly, urx, ury) as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 520);

            // Create a StrikeOutAnnotation on the specified page and rectangle
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect)
            {
                // Set the author of the annotation
                Title = reviewer,
                // Optional: set the color of the strikeout line
                Color = Aspose.Pdf.Color.Red
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added and saved to '{outputPath}'.");
    }
}