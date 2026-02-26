using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a Highlight annotation (a concrete TextMarkupAnnotation).
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set the annotation's appearance properties.
            highlight.Color = Aspose.Pdf.Color.Yellow;               // Highlight color.
            highlight.Opacity = 0.5;                                 // Semi‑transparent.
            highlight.Contents = "**Important**: Review this section."; // Markdown‑style text.

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(highlight);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}