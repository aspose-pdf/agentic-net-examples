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
        const string targetId = "YOUR_ANNOTATION_ID"; // replace with the actual Id (Annotation.Name)

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

            // Pages are 1‑based; page two is accessed with index 2
            Page page = doc.Pages[2];

            Annotation annotationToDelete = null;

            // Iterate through annotations on the page to find the one with the matching Name (Id)
            foreach (Annotation ann in page.Annotations)
            {
                // In Aspose.Pdf for .NET the unique identifier is the Name property
                if (ann.Name == targetId)
                {
                    annotationToDelete = ann;
                    break;
                }
            }

            // If the annotation was found, delete it from the collection
            if (annotationToDelete != null)
            {
                page.Annotations.Delete(annotationToDelete);
                Console.WriteLine($"Annotation with Id '{targetId}' deleted from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Id '{targetId}' not found on page 2.");
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
