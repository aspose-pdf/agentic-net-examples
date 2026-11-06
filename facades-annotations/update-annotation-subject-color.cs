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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page and one annotation
            if (doc.Pages.Count < 1 || doc.Pages[1].Annotations.Count < 1)
            {
                Console.WriteLine("No annotations found to modify.");
                return;
            }

            // Get the first annotation on the first page (1‑based indexing)
            Annotation existing = doc.Pages[1].Annotations[1];

            // Preserve the rectangle of the existing annotation
            Aspose.Pdf.Rectangle rect = existing.Rect;

            // Create a new TextAnnotation with the same rectangle but new subject and color
            TextAnnotation newAnnotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "New Subject",                 // Subject / title
                Contents = existing.Contents,           // Preserve original contents
                Color = Aspose.Pdf.Color.FromRgb(255, 0, 0) // Red color (RGB 0‑255)
            };

            // Replace the old annotation with the new one
            // Remove the existing annotation first
            doc.Pages[1].Annotations.Delete(1); // 1‑based index of the annotation to delete
            // Add the new annotation
            doc.Pages[1].Annotations.Add(newAnnotation);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation updated and saved to '{outputPath}'.");
    }
}
