using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationDeletionBenchmark
{
    static void Main()
    {
        // Paths – replace with actual files when running the benchmark
        const string sourcePdf = "source_with_annotations.pdf";
        const string deleteAllOutput = "deleted_all.pdf";
        const string deleteOneOutput = "deleted_one.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Benchmark: DeleteAnnotations() – removes all annotations at once
        // -----------------------------------------------------------------
        using (PdfAnnotationEditor editorAll = new PdfAnnotationEditor())
        {
            // Load the PDF into the facade
            editorAll.BindPdf(sourcePdf);

            // Measure the deletion time
            Stopwatch swAll = Stopwatch.StartNew();
            editorAll.DeleteAnnotations();               // deletes all annotations
            swAll.Stop();

            // Save the modified PDF
            editorAll.Save(deleteAllOutput);

            Console.WriteLine($"DeleteAnnotations() removed all annotations in {swAll.ElapsedMilliseconds} ms.");
        }

        // -----------------------------------------------------------------
        // Benchmark: DeleteAnnotation(string) – removes a single annotation by name
        // -----------------------------------------------------------------
        // First obtain the name of an existing annotation (any annotation will do)
        string annotationName;
        using (PdfAnnotationEditor tempEditor = new PdfAnnotationEditor())
        {
            tempEditor.BindPdf(sourcePdf);
            // Access the first page's annotation collection
            AnnotationCollection annColl = tempEditor.Document.Pages[1].Annotations;
            if (annColl.Count == 0)
            {
                Console.Error.WriteLine("No annotations found in the source PDF.");
                return;
            }
            // Retrieve the name of the first annotation
            annotationName = annColl[1].Name; // Annotations collection is 1‑based
        }

        using (PdfAnnotationEditor editorOne = new PdfAnnotationEditor())
        {
            editorOne.BindPdf(sourcePdf);

            // Measure the deletion time for a single annotation
            Stopwatch swOne = Stopwatch.StartNew();
            editorOne.DeleteAnnotation(annotationName);   // deletes the specific annotation
            swOne.Stop();

            // Save the modified PDF
            editorOne.Save(deleteOneOutput);

            Console.WriteLine($"DeleteAnnotation(\"{annotationName}\") removed one annotation in {swOne.ElapsedMilliseconds} ms.");
        }
    }
}