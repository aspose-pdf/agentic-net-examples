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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least one page and one annotation
        if (doc.Pages.Count == 0 || doc.Pages[1].Annotations.Count == 0)
        {
            Console.WriteLine("No annotations found to modify.");
            return;
        }

        // Get the first annotation on the first page (assumed to be a TextAnnotation)
        Page page = doc.Pages[1];
        TextAnnotation existing = page.Annotations[1] as TextAnnotation;
        if (existing == null)
        {
            Console.WriteLine("The first annotation is not a TextAnnotation.");
            return;
        }

        // Remove the old annotation
        page.Annotations.Delete(1);

        // Create a new TextAnnotation with the same rectangle but new subject and color
        TextAnnotation newAnno = new TextAnnotation(page, existing.Rect)
        {
            Subject = "Updated Subject",
            Color   = Aspose.Pdf.Color.FromRgb(255, 0, 0) // red
        };

        // Add the new annotation to the page
        page.Annotations.Add(newAnno);

        // Save the modified document
        doc.Save(outputPath);
        Console.WriteLine($"Annotation updated and saved to '{outputPath}'.");
    }
}
