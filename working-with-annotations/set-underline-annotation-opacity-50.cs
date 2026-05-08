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
        const int targetPageNumber = 1; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Iterate through all annotations on the page
            for (int i = 1; i <= page.Annotations.Count; i++)
            {
                Annotation ann = page.Annotations[i];
                // Check for underline annotation
                if (ann is UnderlineAnnotation underline)
                {
                    // Set opacity to 50%
                    underline.Opacity = 0.5;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation opacity set to 50% and saved to '{outputPath}'.");
    }
}