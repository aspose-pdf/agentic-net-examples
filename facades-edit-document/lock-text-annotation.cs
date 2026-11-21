using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // existing PDF (or use a new empty PDF)
        const string outputPath = "locked_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to annotate
            Page page = doc.Pages[1];

            // Define the annotation rectangle
            var rect = new Rectangle(100, 500, 300, 550);

            // Create a text annotation
            var textAnnotation = new TextAnnotation(page, rect)
            {
                Title = "Important Note",
                Contents = "This annotation is locked."
            };

            // Lock the annotation – use the Flags property with AnnotationFlags.Locked
            textAnnotation.Flags = AnnotationFlags.Locked;

            // Add the annotation to the page's collection
            page.Annotations.Add(textAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with locked annotation saved to '{outputPath}'.");
    }
}
