using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Added namespace for Annotation

class AnnotationDeletionBenchmark
{
    static void Main()
    {
        const string sourcePdf = "sample_with_annotations.pdf";
        const string allDeletedPdf = "all_deleted.pdf";
        const string singleDeletedPdf = "single_deleted.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // ------------------------------------------------------------
        // Obtain the name of the first annotation (used for single delete)
        // ------------------------------------------------------------
        string firstAnnotationName;
        using (Document doc = new Document(sourcePdf))
        {
            // Assume at least one page and one annotation exist
            if (doc.Pages.Count == 0 || doc.Pages[1].Annotations.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any annotations to benchmark.");
                return;
            }

            // Annotations are 1‑based indexed
            Annotation firstAnnotation = doc.Pages[1].Annotations[1];
            firstAnnotationName = firstAnnotation.Name;
        }

        // ------------------------------------------------------------
        // Benchmark DeleteAnnotations (remove all annotations)
        // ------------------------------------------------------------
        Stopwatch swAll = Stopwatch.StartNew();

        // Use PdfAnnotationEditor to delete all annotations
        using (PdfAnnotationEditor editorAll = new PdfAnnotationEditor())
        {
            editorAll.BindPdf(sourcePdf);
            editorAll.DeleteAnnotations();               // delete all annotations
            editorAll.Save(allDeletedPdf);               // save result
        }

        swAll.Stop();
        Console.WriteLine($"DeleteAnnotations (all) elapsed: {swAll.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Benchmark DeleteAnnotation (remove a single annotation by name)
        // ------------------------------------------------------------
        Stopwatch swSingle = Stopwatch.StartNew();

        using (PdfAnnotationEditor editorSingle = new PdfAnnotationEditor())
        {
            editorSingle.BindPdf(sourcePdf);
            editorSingle.DeleteAnnotation(firstAnnotationName); // delete one annotation
            editorSingle.Save(singleDeletedPdf);                // save result
        }

        swSingle.Stop();
        Console.WriteLine($"DeleteAnnotation (single) elapsed: {swSingle.ElapsedMilliseconds} ms");
    }
}
