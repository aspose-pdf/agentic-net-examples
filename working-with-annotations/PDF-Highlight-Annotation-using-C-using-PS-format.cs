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

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that will contain the highlight.
            // Fully qualified type avoids ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the specified page and rectangle.
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the visual appearance of the highlight.
                Color    = Aspose.Pdf.Color.Yellow,
                Contents = "Highlighted text"
            };

            // Attach the annotation to the page.
            page.Annotations.Add(highlight);

            // Save the modified document.
            // NOTE: Aspose.Pdf does NOT support saving to PostScript (PS) format.
            // The document must be saved as PDF (or another supported format).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}