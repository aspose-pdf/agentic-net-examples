using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filtered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the RGB color to match (example: pure red)
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0); // 255,0,0

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a snapshot of the annotations to avoid modifying the collection while iterating
                var annotations = page.Annotations.Cast<Annotation>().ToList();

                foreach (Annotation annotation in annotations)
                {
                    // Check if the annotation's color matches the target color
                    if (annotation.Color != null && annotation.Color.Equals(targetColor))
                    {
                        // Delete the matching annotation from the page's collection
                        page.Annotations.Delete(annotation);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations with the specified color have been removed. Saved to '{outputPath}'.");
    }
}