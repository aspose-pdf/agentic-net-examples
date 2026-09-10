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
        const int targetPageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[targetPageNumber];

            // Iterate over annotations on the page
            for (int i = 1; i <= page.Annotations.Count; i++) // annotation collections are 1‑based
            {
                Annotation ann = page.Annotations[i];

                // Check if the annotation is an underline annotation
                if (ann is UnderlineAnnotation underline)
                {
                    // Set opacity to 50% (value range 0.0 – 1.0)
                    underline.Opacity = 0.5;
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation opacity adjusted and saved to '{outputPath}'.");
    }
}