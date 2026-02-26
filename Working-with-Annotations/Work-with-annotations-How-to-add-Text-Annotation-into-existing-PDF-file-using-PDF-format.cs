using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (sticky note) on the specified page
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",          // Title shown in the annotation window
                Contents = "This is a text annotation added via Aspose.Pdf.", // The note text
                Open     = true,                 // Show the annotation popup open by default
                Icon     = TextIcon.Note          // Icon type (Note, Comment, etc.)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}