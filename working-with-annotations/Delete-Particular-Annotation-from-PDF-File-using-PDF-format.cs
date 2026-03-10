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
        const int pageNumber = 1;          // 1‑based page index
        const int annotationIndex = 2;     // 1‑based annotation index to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF (lifecycle rule: using)
            using (Document doc = new Document(inputPath))
            {
                // Ensure the page number is valid (pages are 1‑based)
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid page number.");
                    return;
                }

                // Get the annotation collection for the specified page
                AnnotationCollection annColl = doc.Pages[pageNumber].Annotations;

                // Ensure the annotation index is valid (AnnotationCollection is 1‑based)
                if (annotationIndex < 1 || annotationIndex > annColl.Count)
                {
                    Console.Error.WriteLine("Annotation index out of range.");
                    return;
                }

                // Delete the annotation by its index
                annColl.Delete(annotationIndex);

                // Save the modified PDF (lifecycle rule: using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Deleted annotation #{annotationIndex} on page {pageNumber}. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}