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

        try
        {
            // Load the PDF document.
            using (Document pdf = new Document(inputPath))
            {
                // Page numbers are 1‑based in Aspose.Pdf.
                int pageNumber = 2;
                int annotationIndex = 1; // zero‑based index on the page.

                // Validate the requested page.
                if (pageNumber < 1 || pageNumber > pdf.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                    return;
                }

                Page page = pdf.Pages[pageNumber];

                // Validate the annotation index.
                if (annotationIndex < 0 || annotationIndex >= page.Annotations.Count)
                {
                    Console.Error.WriteLine($"Annotation index {annotationIndex} does not exist on page {pageNumber}.");
                    return;
                }

                // Retrieve the annotation.
                Annotation annotation = page.Annotations[annotationIndex];

                // The Subject property is defined on MarkupAnnotation (and its derived types).
                if (annotation is MarkupAnnotation markup)
                {
                    markup.Subject = "Updated";
                }
                else
                {
                    Console.Error.WriteLine("The selected annotation does not support the Subject property.");
                    return;
                }

                // Save the modified PDF.
                pdf.Save(outputPath);
            }

            Console.WriteLine($"Annotation updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
