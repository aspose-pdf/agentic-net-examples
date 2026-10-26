using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    targetPage = 2; // 1‑based page index to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {targetPage}");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPage];

            // Iterate over all annotations on the page
            foreach (Annotation annot in page.Annotations)
            {
                // Check if the annotation is an underline annotation
                if (annot is UnderlineAnnotation underline)
                {
                    // Set opacity to 50% (value range 0.0‑1.0)
                    underline.Opacity = 0.5f;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation opacity adjusted and saved to '{outputPath}'.");
    }
}