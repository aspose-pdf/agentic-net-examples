using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the target page exists (pages are 1‑based)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Target page number out of range.");
                return;
            }

            // Retrieve the target page
            Page page = doc.Pages[targetPageNumber];

            // Iterate over all annotations on the page
            foreach (Annotation annot in page.Annotations)
            {
                // Process only underline annotations
                if (annot is UnderlineAnnotation underline)
                {
                    // Opacity range is 0.0 (transparent) to 1.0 (opaque)
                    underline.Opacity = 0.5f; // 50 % opacity
                }
            }

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation opacity set to 50 % and saved to '{outputPath}'.");
    }
}