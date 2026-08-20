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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            // Retrieve page 4 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[4];

            // Iterate over all annotations on the page and output their types
            foreach (Annotation annotation in page.Annotations)
            {
                // AnnotationType enum provides a readable type name
                Console.WriteLine($"Annotation Type: {annotation.AnnotationType}");
            }
        }
    }
}