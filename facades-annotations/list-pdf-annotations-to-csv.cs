using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationLister
{
    // Escapes a CSV field according to RFC 4180
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n");
        if (mustQuote)
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputCsvPath = "annotations.csv";   // CSV result

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create the facade and bind the PDF (load)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdfPath);

        // Access the underlying Document to enumerate pages and annotations
        Document doc = editor.Document;

        using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
        {
            // Write CSV header
            csvWriter.WriteLine("AnnotationName,PageNumber");

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annCollection = page.Annotations;

                // Annotation collections are also 1‑based
                for (int annIndex = 1; annIndex <= annCollection.Count; annIndex++)
                {
                    Annotation annotation = annCollection[annIndex];
                    string name = annotation.Name ?? string.Empty;
                    csvWriter.WriteLine($"{EscapeCsv(name)},{pageIndex}");
                }
            }
        }

        // No modifications were made, so no need to save the PDF.
        // If a save were required, it would be:
        // editor.Save("output.pdf");

        Console.WriteLine($"Annotation list written to '{outputCsvPath}'.");
    }
}