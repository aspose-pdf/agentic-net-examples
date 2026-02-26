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
            // Example: work with the first page (Aspose.Pdf uses 1‑based indexing)
            int pageNumber = 1;
            if (pageNumber > doc.Pages.Count)
            {
                Console.WriteLine("Requested page does not exist.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Retrieve the collection of annotations on the page
            AnnotationCollection annotations = page.Annotations;

            Console.WriteLine($"Page {pageNumber} contains {annotations.Count} annotation(s).");

            // Iterate through each annotation and display basic information
            foreach (Annotation annotation in annotations)
            {
                Console.WriteLine($"- Type    : {annotation.AnnotationType}");
                Console.WriteLine($"  Contents: {annotation.Contents}");
                Console.WriteLine($"  Rect    : {annotation.Rect}");
            }
        }
    }
}