using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";
        const string annotationId = "myAnnotation"; // the Id (Name) of the annotation to modify

        // New rectangle coordinates (lower‑left X/Y, upper‑right X/Y)
        double llx = 100; // left
        double lly = 500; // bottom
        double urx = 300; // right
        double ury = 600; // top

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages to locate the annotation by its Id (Name)
            Annotation targetAnnotation = null;
            foreach (Page page in doc.Pages)
            {
                // FindByName returns the annotation with the specified Name (used as Id)
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

            // Modify the rectangle of the found annotation
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
            targetAnnotation.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Save the modified PDF (lifecycle rule: Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation '{annotationId}' rectangle updated and saved to '{outputPath}'.");
    }
}