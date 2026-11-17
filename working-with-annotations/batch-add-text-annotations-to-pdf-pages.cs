using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation, set its properties, and add it to the page
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title    = $"Page {i} Note",
                    Contents = $"Annotation on page {i}",
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                page.Annotations.Add(annotation);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotations added to each page. Saved as '{outputPath}'.");
    }
}