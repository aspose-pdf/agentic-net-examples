using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string xfdfPath       = "annotations.xfdf"; // intermediate XFDF file
        const string outputPdfPath  = "output.pdf";     // final PDF with imported annotations

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Export selected annotation types to XFDF
        // -------------------------------------------------
        using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
        {
            // Bind the source PDF
            exporter.BindPdf(inputPdfPath);

            // Define page range (1‑based indexing)
            int startPage = 1;
            int endPage   = exporter.Document.Pages.Count;

            // Choose which annotation types to export.
            // Here we export Text and Highlight annotations as an example.
            string[] annotTypes = new[]
            {
                AnnotationType.Text.ToString(),
                AnnotationType.Highlight.ToString()
            };

            // Write XFDF to a file
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                exporter.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, annotTypes);
            }
        }

        // -------------------------------------------------
        // Import all annotations from the XFDF file back into a PDF
        // -------------------------------------------------
        using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
        {
            // Bind the same (or another) PDF where annotations will be added
            importer.BindPdf(inputPdfPath);

            // Import all annotations from the XFDF file
            importer.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting PDF
            importer.Save(outputPdfPath);
        }

        Console.WriteLine($"Exported annotations to '{xfdfPath}' and saved imported PDF as '{outputPdfPath}'.");
    }
}