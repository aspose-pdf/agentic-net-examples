using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "annotations_report.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("AnnotationId,Author");

                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    AnnotationCollection annotations = page.Annotations;

                    // Iterate through annotations (1‑based indexing)
                    for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                    {
                        Annotation ann = annotations[annIndex];
                        // Use the Name property as the identifier (Annotation.Id does not exist)
                        string id = ann.Name ?? string.Empty;

                        // Most markup annotations expose the author via the Title property
                        string author = string.Empty;
                        if (ann is MarkupAnnotation markup)
                        {
                            author = markup.Title ?? string.Empty;
                        }

                        // Escape commas in fields if necessary
                        id = id.Replace(",", "\\,");
                        author = author.Replace(",", "\\,");

                        writer.WriteLine($"{id},{author}");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation audit CSV generated at '{csvPath}'.");
    }
}
