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
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Create CSV file for the audit report
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // CSV header
                writer.WriteLine("AnnotationId,Author");

                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through all annotations on the page (1‑based indexing)
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation ann = page.Annotations[annIndex];

                        // Retrieve the annotation identifier – use Name if available (Id property does not exist)
                        string id = ann.Name ?? string.Empty;

                        // Retrieve the author. For markup annotations the Subject property is commonly used as author.
                        string author = string.Empty;
                        if (ann is MarkupAnnotation markup)
                        {
                            author = markup.Subject ?? string.Empty;
                        }
                        else
                        {
                            // Fallback to Contents if Subject is not available
                            author = ann.Contents ?? string.Empty;
                        }

                        // Escape double quotes for CSV compliance
                        id = id.Replace("\"", "\"\"");
                        author = author.Replace("\"", "\"\"");

                        // Write CSV line
                        writer.WriteLine($"\"{id}\",\"{author}\"");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation audit CSV generated at: {csvPath}");
    }
}
