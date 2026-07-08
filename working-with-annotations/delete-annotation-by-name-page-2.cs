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
        const string annotationName = "unique-id"; // replace with the actual Name of the annotation

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 2 exists (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            Page pageTwo = doc.Pages[2]; // page indexing is one‑based

            // Locate the annotation with the specified Name (Annotation.Id does not exist)
            Annotation target = null;
            foreach (Annotation ann in pageTwo.Annotations)
            {
                // Annotation.Name can be used as a unique identifier
                if (string.Equals(ann.Name, annotationName, StringComparison.Ordinal))
                {
                    target = ann;
                    break;
                }
            }

            if (target != null)
            {
                // Delete the annotation from the collection (AnnotationCollection.Delete(Annotation))
                pageTwo.Annotations.Delete(target);
                Console.WriteLine($"Deleted annotation with Name '{annotationName}' from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Name '{annotationName}' not found on page 2.");
            }

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
