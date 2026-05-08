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
        const string annotationId = "MyAnnotationId"; // the Name of the annotation to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Try to find the annotation by its Name (used as Id)
                    Annotation ann = page.Annotations.FindByName(annotationId);
                    if (ann != null)
                    {
                        // Create a new rectangle with desired coordinates
                        // (llx, lly, urx, ury) in user space units
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                        // Update the annotation rectangle
                        ann.Rect = newRect;

                        // If you need to consider page rotation when setting the rectangle,
                        // you can also use the GetRectangle method with true/false as needed.
                        // Example: var actualRect = ann.GetRectangle(true);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotation '{annotationId}' rectangle updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}