using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define annotations: page number, rectangle, title, and content
            var annotations = new[]
            {
                new { Page = 1, Rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750), Title = "Note 1", Content = "First annotation on page 1." },
                new { Page = 2, Rect = new Aspose.Pdf.Rectangle(50, 600, 250, 650), Title = "Reminder", Content = "Second annotation on page 2." },
                new { Page = 3, Rect = new Aspose.Pdf.Rectangle(150, 500, 350, 550), Title = "Info", Content = "Third annotation on page 3." }
            };

            foreach (var a in annotations)
            {
                // Validate page index (Aspose.Pdf uses 1‑based indexing)
                if (a.Page < 1 || a.Page > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping annotation for non‑existent page {a.Page}.");
                    continue;
                }

                Page page = doc.Pages[a.Page];

                // Create a TextAnnotation with the specified rectangle
                TextAnnotation txtAnn = new TextAnnotation(page, a.Rect)
                {
                    Title    = a.Title,
                    Contents = a.Content,
                    Open     = true,               // open the annotation window by default
                    Icon     = TextIcon.Note        // standard note icon
                };

                // Add the annotation to the page
                page.Annotations.Add(txtAnn);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}