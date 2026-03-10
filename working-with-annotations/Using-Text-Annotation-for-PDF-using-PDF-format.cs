using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_text_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the sticky note will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (sticky note) on the specified page
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Review Note",                     // Title shown in the popup title bar
                Contents = "Please verify this section.",    // Text displayed in the popup
                Color    = Aspose.Pdf.Color.Yellow,          // Color of the annotation icon
                Icon     = TextIcon.Note,                    // Icon type (Note, Comment, etc.)
                Open     = false                             // Initially closed (popup hidden)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Save the modified PDF (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with text annotation: '{outputPath}'");
    }
}