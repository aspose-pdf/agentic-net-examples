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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create CSV file and write header
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
            {
                csvWriter.WriteLine("PageNumber,AnnotationName");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // AnnotationCollection is also 1‑based
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];
                        // Use the annotation's Name (or FullName) as its identifier
                        string annName = annotation.Name ?? string.Empty;
                        csvWriter.WriteLine($"{pageIndex},\"{annName}\"");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation list saved to '{outputCsvPath}'.");
    }
}