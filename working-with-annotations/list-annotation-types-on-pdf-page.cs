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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            // Retrieve page 4
            Page pageFour = doc.Pages[4];

            // Iterate over all annotations on the page and output their type names
            foreach (Annotation annotation in pageFour.Annotations)
            {
                Console.WriteLine(annotation.GetType().Name);
            }
        }
    }
}