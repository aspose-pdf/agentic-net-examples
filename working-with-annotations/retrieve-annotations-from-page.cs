using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least four pages (pages are 1‑based)
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Retrieve page 4
            Page page = doc.Pages[4];

            // Get the collection of annotations on this page
            AnnotationCollection annotations = page.Annotations;

            Console.WriteLine($"Page 4 contains {annotations.Count} annotation(s):");

            // Output the type of each annotation
            foreach (Annotation annotation in annotations)
            {
                // AnnotationType enum provides a readable description
                Console.WriteLine($"- {annotation.AnnotationType}");
            }
        }
    }
}