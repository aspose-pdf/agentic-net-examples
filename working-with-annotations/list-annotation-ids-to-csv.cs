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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,AnnotationId");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over all annotations on the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Use the annotation's Name if set; otherwise fall back to FullName
                        string id = annotation.Name ?? annotation.FullName ?? string.Empty;

                        // Write a CSV line for this annotation
                        writer.WriteLine($"{pageIndex},\"{id}\"");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation list written to '{outputCsv}'.");
    }
}