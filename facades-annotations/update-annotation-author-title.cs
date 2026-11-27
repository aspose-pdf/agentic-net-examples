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

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Aspose.Pdf.Document provides full annotation access)
        Document doc = new Document(inputPath);

        // Page numbers and annotation indexes are 1‑based in Aspose.Pdf
        int pageNumber = 2;      // second page
        int annotationIndex = 1; // first annotation on that page (1‑based index)

        // Retrieve the annotation from the specified page
        Annotation ann = doc.Pages[pageNumber].Annotations[annotationIndex];
        if (ann != null)
        {
            // The Title property exists only on markup annotations, so cast accordingly
            if (ann is MarkupAnnotation markup)
            {
                // Use Subject to store the author (Author property does not exist)
                markup.Subject = "Dave";
                markup.Title = "Review";
            }
            else
            {
                // For non‑markup annotations you can still modify generic properties such as Contents
                ann.Contents = "Review";
            }
        }
        else
        {
            Console.Error.WriteLine($"No annotation found at page {pageNumber}, index {annotationIndex}.");
        }

        // Save the modified PDF to a new file
        doc.Save(outputPath);
        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
