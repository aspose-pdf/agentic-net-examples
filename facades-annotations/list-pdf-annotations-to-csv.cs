using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "annotations.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all annotations from the whole document
            int startPage = 1;
            int endPage = editor.Document.Pages.Count;
            IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, (AnnotationType[])null);

            // Write annotation name and page number to CSV
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("AnnotationName,PageNumber"); // header

                foreach (Annotation annot in annotations)
                {
                    string name = annot.Name ?? string.Empty;
                    int pageNumber = annot.PageIndex; // 1‑based page index
                    writer.WriteLine($"{EscapeCsv(name)},{pageNumber}");
                }
            }

            // Close the editor (optional, as using will dispose)
            editor.Close();
        }

        Console.WriteLine($"Annotations list saved to '{outputCsv}'.");
    }

    // Helper to escape CSV fields containing commas, quotes or newlines
    static string EscapeCsv(string field)
    {
        if (field.Contains("\"") || field.Contains(",") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}