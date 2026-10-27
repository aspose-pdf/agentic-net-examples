using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class AnnotationDeletionBenchmark
{
    // Number of annotations to create for the test PDF
    const int AnnotationCount = 200;

    // Paths for temporary files
    static readonly string BaseDir = Path.Combine(Path.GetTempPath(), "AsposePdfBenchmark");
    static readonly string OriginalPdf = Path.Combine(BaseDir, "original.pdf");
    static readonly string DeleteOnePdf = Path.Combine(BaseDir, "delete_one.pdf");
    static readonly string DeleteAllPdf = Path.Combine(BaseDir, "delete_all.pdf");

    static void Main()
    {
        // Ensure clean working directory
        if (Directory.Exists(BaseDir))
            Directory.Delete(BaseDir, true);
        Directory.CreateDirectory(BaseDir);

        // -----------------------------------------------------------------
        // 1. Create a sample PDF with many text annotations
        // -----------------------------------------------------------------
        CreateSamplePdfWithAnnotations();

        // -----------------------------------------------------------------
        // 2. Benchmark DeleteAnnotation (single annotation removal)
        // -----------------------------------------------------------------
        // Work on a fresh copy of the original PDF
        File.Copy(OriginalPdf, DeleteOnePdf, true);
        long deleteOneMs = BenchmarkDeleteSingleAnnotation(DeleteOnePdf);
        Console.WriteLine($"DeleteAnnotation (single) elapsed: {deleteOneMs} ms");

        // -----------------------------------------------------------------
        // 3. Benchmark DeleteAnnotations (removes all annotations on a page)
        // -----------------------------------------------------------------
        // Work on another fresh copy of the original PDF
        File.Copy(OriginalPdf, DeleteAllPdf, true);
        long deleteAllMs = BenchmarkDeleteAllAnnotations(DeleteAllPdf);
        Console.WriteLine($"DeleteAnnotations (all) elapsed: {deleteAllMs} ms");
    }

    // Creates a one‑page PDF and adds a number of text annotations.
    static void CreateSamplePdfWithAnnotations()
    {
        // Use a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Add some placeholder text so the page is not empty
            page.Paragraphs.Add(new TextFragment("Sample PDF for annotation deletion benchmark."));

            // Add a series of text annotations at different positions
            for (int i = 0; i < AnnotationCount; i++)
            {
                double llx = 50;
                double lly = 750 - i * 3;   // lower‑left y
                double urx = 300;
                double ury = lly + 15;      // upper‑right y

                Rectangle rect = new Rectangle(llx, lly, urx, ury);
                string title = $"Note {i + 1}";
                string contents = $"Annotation #{i + 1}";

                // Create a text annotation using the (Page, Rectangle) constructor
                TextAnnotation txtAnnot = new TextAnnotation(page, rect)
                {
                    Title = title,
                    Contents = contents
                };
                page.Annotations.Add(txtAnnot);
            }

            // Save the PDF that now contains all annotations
            doc.Save(OriginalPdf);
        }
    }

    // Measures the time required to delete a single annotation using the Document API
    static long BenchmarkDeleteSingleAnnotation(string pdfPath)
    {
        using (Document doc = new Document(pdfPath))
        {
            Page page = doc.Pages[1];
            if (page.Annotations == null || page.Annotations.Count == 0)
                throw new InvalidOperationException("No annotations found for deletion test.");

            // Capture the first annotation reference (collections are 1‑based)
            Annotation firstAnnot = page.Annotations[1];

            Stopwatch sw = Stopwatch.StartNew();
            page.Annotations.Delete(firstAnnot);
            sw.Stop();

            // Save the modified PDF (ensures the operation is fully executed)
            doc.Save(pdfPath);
            return sw.ElapsedMilliseconds;
        }
    }

    // Measures the time required to delete all annotations on a page using the Document API
    static long BenchmarkDeleteAllAnnotations(string pdfPath)
    {
        using (Document doc = new Document(pdfPath))
        {
            Page page = doc.Pages[1];

            Stopwatch sw = Stopwatch.StartNew();
            page.Annotations.Clear();
            sw.Stop();

            // Save the modified PDF (ensures the operation is fully executed)
            doc.Save(pdfPath);
            return sw.ElapsedMilliseconds;
        }
    }
}
