using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationLister
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "annotations.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF using PdfAnnotationEditor (Facades API)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            editor.BindPdf(inputPdf);
            Document doc = editor.Document; // underlying Document instance

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("AnnotationName,PageNumber");

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    // Each page has an AnnotationCollection
                    foreach (Annotation ann in page.Annotations)
                    {
                        string name = ann.Name ?? string.Empty;
                        // Simple CSV escaping
                        if (name.Contains("\"") || name.Contains(","))
                        {
                            name = $"\"{name.Replace("\"", "\"\"")}\"";
                        }
                        writer.WriteLine($"{name},{pageNum}");
                    }
                }
            }

            Console.WriteLine($"Annotation list saved to '{outputCsv}'.");
        }
        finally
        {
            // Release resources held by the facade
            editor.Close();
        }
    }
}