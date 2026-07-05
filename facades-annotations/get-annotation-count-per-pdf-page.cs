using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class AnnotationReporter
{
    // Returns a dictionary where the key is the 1‑based page number
    // and the value is the total number of annotations on that page.
    public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);

        var result = new Dictionary<int, int>();

        // Facade for working with PDF annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document (facade load rule)
            editor.BindPdf(pdfPath);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate through pages (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                int count = page.Annotations.Count; // AnnotationCollection.Count property
                result[i] = count;
            }
        }

        return result;
    }
}

// Example usage
class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        // Ensure the sample PDF exists – create a minimal document if it does not.
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
        }

        var counts = AnnotationReporter.GetAnnotationsCountPerPage(inputPdf);

        foreach (var kvp in counts)
        {
            Console.WriteLine($"Page {kvp.Key}: {kvp.Value} annotation(s)");
        }
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a simple PDF with one page and a sample text annotation.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple text annotation so the count is not always zero.
            var textAnnotation = new Aspose.Pdf.Annotations.TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Title = "Sample",
                Contents = "This is a sample annotation",
                Color = Color.Yellow
            };
            page.Annotations.Add(textAnnotation);

            doc.Save(path);
        }
    }
}
