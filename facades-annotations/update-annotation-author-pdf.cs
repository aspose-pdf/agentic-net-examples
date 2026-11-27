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

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Aspose.Pdf uses 1‑based page indexing
        int pageNumber = 1;
        Page page = doc.Pages[pageNumber];

        // Ensure the page contains at least one annotation
        if (page.Annotations == null || page.Annotations.Count == 0)
        {
            Console.WriteLine("No annotations found on page 1.");
        }
        else
        {
            // The first annotation (index 0)
            Annotation annotation = page.Annotations[0];

            // The Author/Title property is defined on MarkupAnnotation (e.g., TextAnnotation)
            if (annotation is MarkupAnnotation markup)
            {
                // Change the author (Title) to "Carol"
                markup.Title = "Carol";
                Console.WriteLine("Annotation author changed to 'Carol'.");
            }
            else
            {
                Console.WriteLine("First annotation is not a markup annotation; cannot set author.");
            }
        }

        // Save the updated PDF
        doc.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
