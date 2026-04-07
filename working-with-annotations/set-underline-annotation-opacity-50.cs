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
        const int targetPageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Target page number is out of range.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Iterate through all annotations on the page (annotation collections are 1‑based)
            for (int i = 1; i <= page.Annotations.Count; i++)
            {
                Annotation ann = page.Annotations[i];

                // Identify underline annotations
                if (ann is UnderlineAnnotation underline)
                {
                    // Set opacity to 50% (range 0.0 – 1.0)
                    underline.Opacity = 0.5;
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation opacity set to 50% and saved to '{outputPath}'.");
    }
}