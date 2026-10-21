using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Target RGB color (values 0‑1 for Aspose.Pdf.Color)
        // Example: pure red (1.0, 0.0, 0.0)
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the core API (Document) for color inspection and modification
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Get the annotation collection for the current page
                var pageAnnotations = doc.Pages[pageNum].Annotations;

                // Iterate backwards so that removal does not affect the loop index
                for (int i = pageAnnotations.Count; i >= 1; i--)
                {
                    var annotation = pageAnnotations[i];

                    // Compare the annotation's color with the target color
                    if (annotation.Color != null && annotation.Color.Equals(targetColor))
                    {
                        // Delete the annotation directly from the collection (1‑based index)
                        pageAnnotations.Delete(i);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations with the specified color have been removed. Output saved to '{outputPath}'.");
    }
}
