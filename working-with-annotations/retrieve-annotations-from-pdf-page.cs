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
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[4];

            // Retrieve the collection of annotations on page four
            AnnotationCollection annotations = page.Annotations;

            Console.WriteLine($"Page 4 contains {annotations.Count} annotation(s):");

            // Iterate through each annotation and output its type
            foreach (Annotation annotation in annotations)
            {
                // AnnotationType enum provides a readable type name
                Console.WriteLine(annotation.AnnotationType);
            }
        }
    }
}