using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "annotations.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create CSV file and write header
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
            {
                csvWriter.WriteLine("PageNumber,AnnotationName,AnnotationType");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    AnnotationCollection annotations = page.Annotations;

                    // AnnotationCollection is also 1‑based
                    for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                    {
                        Annotation annotation = annotations[annIndex];

                        // Use the Name property as the identifier (Id does not exist)
                        string name = annotation.Name ?? string.Empty;
                        string type = annotation.AnnotationType.ToString();

                        // Write a CSV line for each annotation
                        csvWriter.WriteLine($"{pageIndex},\"{name}\",\"{type}\"");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation list written to '{outputCsvPath}'.");
    }
}