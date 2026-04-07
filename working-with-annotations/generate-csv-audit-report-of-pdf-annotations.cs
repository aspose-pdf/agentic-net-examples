using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationAuditReport
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "annotation_audit.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            StringBuilder sb = new StringBuilder();
            // CSV header
            sb.AppendLine("AnnotationId,Author");

            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Use the annotation's Name as an identifier (Id does not exist)
                    string id = annotation.Name ?? string.Empty;

                    // Title (author) exists only on markup annotations
                    string author = string.Empty;
                    if (annotation is MarkupAnnotation markup)
                    {
                        author = markup.Title ?? string.Empty;
                    }

                    // Escape commas and quotes for CSV compliance
                    id = EscapeCsv(id);
                    author = EscapeCsv(author);

                    sb.AppendLine($"{id},{author}");
                }
            }

            // Write CSV content to file
            File.WriteAllText(outputCsvPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Annotation audit report saved to '{outputCsvPath}'.");
    }

    // Helper to escape CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field.Contains('"'))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            field = $"\"{field}\"";

        return field;
    }
}
