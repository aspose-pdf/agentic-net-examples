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

        // Verify input file exists
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

                // Define the annotation rectangle (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a text annotation for the current page
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title    = $"Page {i}",
                    Contents = $"Annotation on page {i}",
                    Color    = Aspose.Pdf.Color.Yellow, // Background color of the annotation icon
                    Open     = true,                    // Open the annotation pop‑up by default
                    Icon     = TextIcon.Note            // Use the standard note icon
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(txtAnn);
            }

            // Save the modified document (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotations added to each page. Saved as '{outputPath}'.");
    }
}