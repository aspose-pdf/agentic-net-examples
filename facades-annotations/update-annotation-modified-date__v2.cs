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

        // Page numbers are 1‑based; annotation index is zero‑based
        int pageNumber = 5;
        int annotationIndex = 3;

        // Validate page number
        if (pageNumber < 1 || pageNumber > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
            return;
        }

        // Retrieve the page and its annotations
        Page page = doc.Pages[pageNumber];
        Annotation[] annotations = page.Annotations.ToArray();

        if (annotations == null || annotations.Length <= annotationIndex)
        {
            Console.Error.WriteLine($"Annotation index {annotationIndex} does not exist on page {pageNumber}.");
            return;
        }

        // Modify the selected annotation's Modified date (use MarkupAnnotation.Modified)
        Annotation target = annotations[annotationIndex];
        if (target is MarkupAnnotation markup)
        {
            markup.Modified = DateTime.Now;
        }
        else
        {
            // For non‑markup annotations the Modified property is not available; skip or handle accordingly
            Console.Error.WriteLine("Selected annotation is not a markup annotation; Modified date cannot be set.");
            return;
        }

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Annotation updated and PDF saved to '{outputPath}'.");
    }
}
