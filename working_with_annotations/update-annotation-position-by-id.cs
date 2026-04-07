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
        const string annotationId = "myAnnotation"; // Identifier stored in the annotation's Name property

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Annotation target = null;

            // Search all pages for the annotation with the specified Id (Name)
            foreach (Page page in doc.Pages)
            {
                target = page.Annotations.FindByName(annotationId);
                if (target != null)
                    break;
            }

            if (target == null)
            {
                Console.Error.WriteLine($"Annotation with Id '{annotationId}' not found.");
                return;
            }

            // Define the new rectangle coordinates (llx, lly, urx, ury)
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Update the annotation's rectangle
            target.Rect = newRect;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation updated and saved to '{outputPath}'.");
    }
}