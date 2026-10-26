using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "annotations.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,AnnotationName");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over all annotations on the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Use the Name property as the identifier (Id does not exist)
                        string name = annotation.Name ?? string.Empty;
                        writer.WriteLine($"{pageIndex},{name}");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation names have been written to '{outputCsv}'.");
    }
}
