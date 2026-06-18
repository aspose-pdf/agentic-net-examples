using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, target annotation Id and new rectangle coordinates
        const string inputPath  = "input.pdf";
        const string outputPath = "modified.pdf";
        const string annotationId = "MyAnnotationId"; // the Name of the annotation
        // New rectangle: lower‑left x/y and upper‑right x/y
        const double llx = 100;
        const double lly = 500;
        const double urx = 300;
        const double ury = 700;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages to locate the annotation by its Name (Id)
            Annotation targetAnnotation = null;
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                // FindByName returns null if not found on this page
                Annotation ann = page.Annotations.FindByName(annotationId);
                if (ann != null)
                {
                    targetAnnotation = ann;
                    break;
                }
            }

            if (targetAnnotation == null)
            {
                Console.Error.WriteLine($"Annotation with Id '{annotationId}' not found.");
                return;
            }

            // Update the rectangle of the found annotation.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            targetAnnotation.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation '{annotationId}' rectangle updated and saved to '{outputPath}'.");
    }
}