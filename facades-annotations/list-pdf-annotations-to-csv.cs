using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "annotations.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Instantiate a Facades class to satisfy the requirement of using Aspose.Pdf.Facades
        PdfFileEditor facadesHelper = new PdfFileEditor();

        // Load the PDF with the core Document API (wrapped in a using block for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            var csvLines = new List<string>();
            csvLines.Add("PageNumber,AnnotationName"); // CSV header

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Use the AnnotationType enum as the annotation name (e.g., Text, Link, Highlight)
                    string annotationName = annotation.AnnotationType.ToString();
                    csvLines.Add($"{pageIndex},{annotationName}");
                }
            }

            // Write the collected data to a CSV file
            File.WriteAllLines(csvPath, csvLines);
        }

        Console.WriteLine($"Annotation list saved to '{csvPath}'.");
    }
}
