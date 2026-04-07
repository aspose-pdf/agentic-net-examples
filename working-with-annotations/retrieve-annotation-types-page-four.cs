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

        // Open the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page page = doc.Pages[4];               // page 4
            AnnotationCollection annotations = page.Annotations;

            Console.WriteLine($"Page 4 contains {annotations.Count} annotation(s).");

            // Output the type of each annotation
            foreach (Annotation ann in annotations)
            {
                // AnnotationType enum gives the specific annotation kind
                Console.WriteLine($"Annotation type: {ann.AnnotationType}");
            }
        }
    }
}