using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvReportPath = "annotations_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create the CSV file (overwrites if it exists)
            using (StreamWriter writer = new StreamWriter(csvReportPath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("AnnotationId,Author");

                // Iterate pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate all annotations on the current page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Use Name as a stable identifier (Id property does not exist)
                        string id = annot.Name ?? string.Empty;

                        // Author information is stored in the Title property of markup annotations.
                        string author = string.Empty;
                        if (annot is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                        {
                            author = markup.Title;
                        }

                        // Write a CSV line, escaping fields as needed
                        writer.WriteLine($"{EscapeCsv(id)},{EscapeCsv(author)}");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation report generated: {csvReportPath}");
    }

    // Simple CSV field escaper: wraps field in quotes if it contains a comma or quote
    static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\""))
            return $"\"{field}\"";

        return field;
    }
}
