using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a highlight annotation, and save it.
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (1‑based index).
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted.
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation.
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the highlight color (e.g., yellow).
                Color = Aspose.Pdf.Color.Yellow,
                // Optional: add a comment that appears in the popup.
                Contents = "Important text highlighted."
            };

            // Add the annotation to the page.
            page.Annotations.Add(highlight);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}