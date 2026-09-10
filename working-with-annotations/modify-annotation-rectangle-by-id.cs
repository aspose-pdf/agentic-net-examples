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
        const string annotationId = "myAnnotation"; // Identifier of the annotation to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            bool found = false;

            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Retrieve the annotation by its Name (Id) using FindByName
                Annotation ann = page.Annotations.FindByName(annotationId);
                if (ann != null)
                {
                    // Get the current rectangle
                    Aspose.Pdf.Rectangle oldRect = ann.Rect;

                    // Example modification: shift 10 units right/down and expand width/height by 20 units
                    double newLlx = oldRect.LLX + 10;
                    double newLly = oldRect.LLY + 10;
                    double newUrx = oldRect.URX + 10 + 20;
                    double newUry = oldRect.URY + 10 + 20;

                    // Assign the new rectangle
                    ann.Rect = new Aspose.Pdf.Rectangle(newLlx, newLly, newUrx, newUry);

                    found = true;
                    break; // Stop after the first matching annotation is updated
                }
            }

            if (!found)
                Console.WriteLine($"Annotation with Id '{annotationId}' not found.");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}