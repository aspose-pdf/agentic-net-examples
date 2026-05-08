using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the result PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Unique identifier of the annotation to be removed (stored in the annotation's Name property)
        const string annotationId = "unique-annotation-id";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a page 2.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based (rule: page-indexing-one-based)
            Page pageTwo = doc.Pages[2];

            // Locate the annotation with the specified Name (Aspose.Pdf does not expose an Id property)
            Annotation annotationToDelete = null;
            foreach (Annotation ann in pageTwo.Annotations)
            {
                // Use the Name property as the unique identifier; guard against null values
                if (!string.IsNullOrEmpty(ann.Name) && ann.Name.Equals(annotationId, StringComparison.Ordinal))
                {
                    annotationToDelete = ann;
                    break;
                }
            }

            // If the annotation exists, delete it from the collection
            if (annotationToDelete != null)
            {
                // AnnotationCollection.Delete(Annotation) removes the specific annotation
                pageTwo.Annotations.Delete(annotationToDelete);
                Console.WriteLine($"Annotation with Id '{annotationId}' deleted from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Id '{annotationId}' not found on page 2.");
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
