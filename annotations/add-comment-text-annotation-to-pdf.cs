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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation with the rectangle
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Comment Title",
                Contents = "This is a comment annotation.",
                Open     = true,
                // Set the icon to the Comment symbol
                Icon     = TextIcon.Comment
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}