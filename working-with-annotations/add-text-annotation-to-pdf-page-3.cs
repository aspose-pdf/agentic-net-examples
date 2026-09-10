using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Retrieve page three
            Page page = doc.Pages[3];

            // Define the annotation rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation on the specified page and rectangle
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note",                                   // Title shown in the annotation window
                Contents = "This is a text annotation on page 3.",// Text displayed in the popup
                Color = Aspose.Pdf.Color.Yellow,                 // Border color of the annotation
                Icon = TextIcon.Note,                             // Icon type (sticky note)
                Open = true                                       // Show the popup open by default
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF back to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}