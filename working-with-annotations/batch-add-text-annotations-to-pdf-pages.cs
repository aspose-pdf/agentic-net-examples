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

                // Define the annotation rectangle (left, bottom, right, top)
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a text annotation on the current page
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title    = $"Page {i} Note",
                    Contents = $"This is a note on page {i}.",
                    Open     = true,                     // Open the popup by default
                    Icon     = TextIcon.Note,            // Standard note icon
                    Color    = Aspose.Pdf.Color.Yellow   // Background color of the annotation
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(annotation);
            }

            // Save the modified document (document-disposal-with-using rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}