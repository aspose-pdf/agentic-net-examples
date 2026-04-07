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
        const string targetAnnotationId = "unique-id-123"; // replace with the actual Id (stored in the annotation's Name property)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page numbers are 1‑based; page 2 is accessed via index 2
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            Page pageTwo = doc.Pages[2];
            AnnotationCollection annotations = pageTwo.Annotations;

            // Locate the annotation with the specified Id (stored in the Name property)
            Annotation annotationToDelete = null;
            foreach (Annotation ann in annotations)
            {
                // Annotation.Name is the supported way to store a custom identifier
                if (string.Equals(ann.Name, targetAnnotationId, StringComparison.Ordinal))
                {
                    annotationToDelete = ann;
                    break;
                }
            }

            if (annotationToDelete != null)
            {
                // Delete the found annotation from the collection
                annotations.Delete(annotationToDelete);
                Console.WriteLine($"Annotation with Id '{targetAnnotationId}' deleted from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Id '{targetAnnotationId}' not found on page 2.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
