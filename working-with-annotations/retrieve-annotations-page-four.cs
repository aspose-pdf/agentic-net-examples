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

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 4 is accessed with index 4
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page pageFour = doc.Pages[4];

            // Retrieve the annotation collection for page four
            AnnotationCollection annotations = pageFour.Annotations;

            Console.WriteLine($"Page 4 contains {annotations.Count} annotation(s):");

            // Iterate over each annotation and output its type (enum value)
            foreach (Annotation annotation in annotations)
            {
                // AnnotationType enum provides a readable type name
                Console.WriteLine($"- {annotation.AnnotationType}");
            }
        }
    }
}