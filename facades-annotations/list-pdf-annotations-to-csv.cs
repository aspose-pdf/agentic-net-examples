using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // PdfAnnotationEditor does NOT implement IDisposable; use explicit Close().
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(inputPdfPath);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Create CSV file and write header.
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                writer.WriteLine("AnnotationName,PageNumber");

                // Iterate through all pages (1‑based indexing).
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through all annotations on the current page.
                    foreach (Annotation annotation in page.Annotations)
                    {
                        string name = annotation.Name ?? string.Empty;
                        // Escape CSV fields that may contain commas, quotes, or newlines.
                        string escapedName = EscapeCsv(name);
                        writer.WriteLine($"{escapedName},{pageIndex}");
                    }
                }
            }

            Console.WriteLine($"Annotation list saved to '{outputCsvPath}'.");
        }
        finally
        {
            // Ensure resources are released.
            editor.Close();
        }
    }

    // Helper to escape CSV fields according to RFC 4180.
    private static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}