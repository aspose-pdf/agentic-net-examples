using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationBenchmark
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Prepare a test PDF with a single text annotation on the first page
        // -----------------------------------------------------------------
        string pdfWithAnnotation = Path.Combine(Path.GetDirectoryName(sourcePdf)!, "with_annotation.pdf");
        File.Copy(sourcePdf, pdfWithAnnotation, true);

        string annotationId;
        using (Document doc = new Document(pdfWithAnnotation))
        {
            // Create a simple text annotation
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1],
                new Rectangle(100, 500, 300, 550))
            {
                Title    = "Sample",
                Contents = "Benchmark annotation",
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(txtAnn);
            doc.Save(pdfWithAnnotation);

            // Store the generated annotation ID for later deletion
            annotationId = txtAnn.Name; // Name property holds the annotation ID
        }

        // --------------------------------------------------------------
        // Create two independent copies for the two benchmark scenarios
        // --------------------------------------------------------------
        string singleDeletePdf = Path.Combine(Path.GetDirectoryName(sourcePdf)!, "single_delete.pdf");
        string allDeletePdf    = Path.Combine(Path.GetDirectoryName(sourcePdf)!, "all_delete.pdf");
        File.Copy(pdfWithAnnotation, singleDeletePdf, true);
        File.Copy(pdfWithAnnotation, allDeletePdf, true);

        // --------------------------------------------------------------
        // Benchmark DeleteAnnotation (single annotation removal)
        // --------------------------------------------------------------
        using (var editorSingle = new PdfAnnotationEditor())
        {
            editorSingle.BindPdf(singleDeletePdf);
            var swSingle = Stopwatch.StartNew();

            // DeleteAnnotation overload that accepts only the annotation name
            editorSingle.DeleteAnnotation(annotationId);
            editorSingle.Save(singleDeletePdf);

            swSingle.Stop();
            Console.WriteLine($"DeleteAnnotation elapsed: {swSingle.ElapsedMilliseconds} ms");
        }

        // --------------------------------------------------------------
        // Benchmark DeleteAnnotations (removes all annotations on the page)
        // --------------------------------------------------------------
        using (var editorAll = new PdfAnnotationEditor())
        {
            editorAll.BindPdf(allDeletePdf);
            var swAll = Stopwatch.StartNew();

            // DeleteAnnotations expects a page number as a string
            editorAll.DeleteAnnotations("1");
            editorAll.Save(allDeletePdf);

            swAll.Stop();
            Console.WriteLine($"DeleteAnnotations elapsed: {swAll.ElapsedMilliseconds} ms");
        }

        // --------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------
        try
        {
            File.Delete(pdfWithAnnotation);
            File.Delete(singleDeletePdf);
            File.Delete(allDeletePdf);
        }
        catch { /* ignore cleanup errors */ }
    }
}
