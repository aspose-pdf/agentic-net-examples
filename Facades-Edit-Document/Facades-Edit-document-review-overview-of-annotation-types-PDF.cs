using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Dictionary to hold annotation type counts
        var annotationCounts = new Dictionary<AnnotationType, int>();

        // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
        for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
        {
            Page page = pdfDocument.Pages[pageIndex];

            // Iterate over annotations on the current page
            foreach (Annotation annotation in page.Annotations)
            {
                AnnotationType type = annotation.AnnotationType;

                if (annotationCounts.ContainsKey(type))
                    annotationCounts[type]++;
                else
                    annotationCounts[type] = 1;
            }
        }

        // Display an overview of annotation types found in the document
        Console.WriteLine("Annotation Overview:");
        if (annotationCounts.Count == 0)
        {
            Console.WriteLine("  No annotations found.");
        }
        else
        {
            foreach (var kvp in annotationCounts)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }

        // Save the (unchanged) document to the output path
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}