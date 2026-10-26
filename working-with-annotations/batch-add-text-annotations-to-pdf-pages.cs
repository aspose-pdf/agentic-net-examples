using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the annotation rectangle (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a text annotation for the current page
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title    = $"Page {i} Note",
                    Contents = $"This is a batch annotation on page {i}.",
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF (still within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotations added to all pages. Saved as '{outputPath}'.");
    }
}