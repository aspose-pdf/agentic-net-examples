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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; page 4 is at index 4
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page pageFour = doc.Pages[4];

            // If there are no annotations, inform the user
            if (pageFour.Annotations == null || pageFour.Annotations.Count == 0)
            {
                Console.WriteLine("Page 4 contains no annotations.");
                return;
            }

            // Iterate over all annotations on page 4 and output their type names
            foreach (Annotation annotation in pageFour.Annotations)
            {
                // Get the concrete annotation type (e.g., TextAnnotation, LinkAnnotation, etc.)
                string typeName = annotation.GetType().Name;
                Console.WriteLine($"Annotation type: {typeName}");
            }
        }
    }
}