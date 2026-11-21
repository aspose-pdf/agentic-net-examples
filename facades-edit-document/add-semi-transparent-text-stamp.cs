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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Page numbers are 1‑based, annotation indexes are 0‑based
        int pageNumber = 1;
        int annotationIndex = 0;

        if (pageNumber <= doc.Pages.Count)
        {
            Page page = doc.Pages[pageNumber];
            if (annotationIndex >= 0 && annotationIndex < page.Annotations.Count)
            {
                Annotation annotation = page.Annotations[annotationIndex];

                // Opacity is defined on MarkupAnnotation (and its derived types)
                if (annotation is MarkupAnnotation markup)
                {
                    // Set opacity to 50% (partially transparent)
                    markup.Opacity = 0.5f;
                }
                else
                {
                    Console.WriteLine("The selected annotation type does not support opacity.");
                }
            }
            else
            {
                Console.WriteLine("No annotation found at the specified index.");
            }
        }
        else
        {
            Console.WriteLine("Page number out of range.");
        }

        // Save the updated PDF
        doc.Save(outputPath);
        Console.WriteLine($"PDF saved with updated annotation opacity to '{outputPath}'.");
    }
}
