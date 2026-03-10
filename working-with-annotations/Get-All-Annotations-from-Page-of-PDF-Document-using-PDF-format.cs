using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "sample.pdf";

        // Page number to retrieve annotations from (1‑based indexing)
        const int pageNumber = 1;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate requested page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the specified page
            Page page = doc.Pages[pageNumber];

            // Retrieve the collection of annotations on the page
            AnnotationCollection annotations = page.Annotations;

            Console.WriteLine($"Page {pageNumber} contains {annotations.Count} annotation(s).");

            // Iterate through all annotations and display basic information
            for (int i = 1; i <= annotations.Count; i++) // AnnotationCollection is 1‑based
            {
                Annotation ann = annotations[i];
                Console.WriteLine($"Annotation {i}:");
                Console.WriteLine($"  Type    : {ann.AnnotationType}");
                Console.WriteLine($"  Name    : {ann.Name}");
                Console.WriteLine($"  Contents: {ann.Contents}");
                Console.WriteLine($"  Color   : {ann.Color}");
                Console.WriteLine($"  Rect    : {ann.Rect}");
            }
        }
    }
}