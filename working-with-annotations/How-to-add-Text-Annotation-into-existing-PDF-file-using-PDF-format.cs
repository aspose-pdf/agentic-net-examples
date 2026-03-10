using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the sticky‑note icon will appear
            // Aspose.Pdf.Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a TextAnnotation (sticky note) on the specified page and rectangle
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",                     // Title shown in the annotation window
                Contents = "This is a sample text annotation added programmatically.", // Popup text
                Color    = Aspose.Pdf.Color.Yellow,          // Color of the annotation icon
                Icon     = TextIcon.Note,                    // Icon type (Note, Comment, etc.)
                Open     = false                             // Whether the annotation is opened by default
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified document to the output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}