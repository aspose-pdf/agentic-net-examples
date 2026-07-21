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
        const string targetAnnotationId = "unique-id-123"; // replace with the actual Id (stored in the annotation's Name property)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based (rule: page-indexing-one-based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            Page pageTwo = doc.Pages[2];
            AnnotationCollection annotations = pageTwo.Annotations;

            // Find the annotation with the specified Id (stored in the Name property)
            Annotation annotationToDelete = null;
            foreach (Annotation ann in annotations)
            {
                // The Name property can be used to store a unique identifier for an annotation
                if (ann.Name != null && ann.Name.Equals(targetAnnotationId, StringComparison.Ordinal))
                {
                    annotationToDelete = ann;
                    break;
                }
            }

            if (annotationToDelete != null)
            {
                // Delete the annotation using the Delete(Annotation) overload
                annotations.Delete(annotationToDelete);
                Console.WriteLine($"Annotation with Id '{targetAnnotationId}' deleted from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Id '{targetAnnotationId}' not found on page 2.");
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
