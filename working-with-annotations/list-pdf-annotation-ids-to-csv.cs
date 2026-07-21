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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare the CSV file (overwrite if it exists)
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,AnnotationName");

                // Iterate pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    AnnotationCollection annotations = page.Annotations;

                    // Iterate all annotations on the current page
                    foreach (Annotation annotation in annotations)
                    {
                        // Use the Name property as the annotation identifier.
                        // If Name is null or empty, fall back to FullName.
                        string id = !string.IsNullOrEmpty(annotation.Name) ? annotation.Name : annotation.FullName ?? string.Empty;

                        // Escape commas in the identifier if necessary
                        if (id.Contains(","))
                            id = $"\"{id}\"";

                        writer.WriteLine($"{pageIndex},{id}");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation list saved to '{outputCsvPath}'.");
    }
}