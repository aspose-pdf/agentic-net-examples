using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string csvPath = "annotations_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            // Open a writer for the CSV output
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // CSV header
                writer.WriteLine("PageNumber,AnnotationId,Author");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Annotations collection is also 1‑based
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // Use the annotation's Name as an identifier (Id property is not available in this version)
                        string id = annotation.Name ?? string.Empty;

                        // The author is stored in the Title property of markup annotations
                        string author = string.Empty;
                        if (annotation is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                        {
                            author = markup.Title;
                        }

                        // Simple CSV escaping for commas
                        id = id.Replace(",", ";");
                        author = author.Replace(",", ";");

                        writer.WriteLine($"{pageIndex},{id},{author}");
                    }
                }
            }

            Console.WriteLine($"CSV report generated: {csvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
