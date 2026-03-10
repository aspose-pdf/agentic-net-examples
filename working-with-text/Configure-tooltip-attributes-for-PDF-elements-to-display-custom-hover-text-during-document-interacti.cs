using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";
        const string tooltip    = "Custom hover text displayed as a tooltip";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the tooltip should appear (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (position and size)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (sticky note) on the specified page and rectangle
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                // The Contents property is shown as a tooltip when the user hovers over the annotation
                Contents = tooltip,
                // Optional: set a title that appears in the annotation's popup window
                Title    = "Note",
                // Optional: set the icon style (e.g., Comment, Note, etc.)
                Icon     = TextIcon.Comment,
                // Optional: make the annotation initially closed (default)
                Open     = false,
                // Optional: set the color of the annotation icon
                Color    = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with tooltip saved to '{outputPath}'.");
    }
}