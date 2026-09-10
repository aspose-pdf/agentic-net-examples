using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying document to enumerate pages
            Document doc = editor.Document;
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Iterate through each page and its annotations
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // AnnotationCollection also uses 1‑based indexing
                for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                {
                    Annotation annotation = annotations[annIndex];
                    // Output the annotation name for debugging
                    Console.WriteLine($"Page {pageIndex}, Annotation Name: {annotation.Name}");
                }
            }
        }
    }
}