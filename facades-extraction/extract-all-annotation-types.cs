using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "AllTypes.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core API (Document model)
        Document doc = new Document(inputPath);

        // Open the output text file
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];

                // Each page contains a collection of Annotation objects
                foreach (Annotation annotation in page.Annotations)
                {
                    // Write the annotation type and the page it belongs to
                    writer.WriteLine($"{annotation.AnnotationType} - Page {pageNumber}");
                }
            }
        }

        Console.WriteLine($"All annotations have been saved to '{outputPath}'.");
    }
}
