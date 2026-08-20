using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationBenchmark
{
    const string SamplePdf = "sample.pdf";
    const string OutputAll = "output_all.pdf";
    const string OutputSingle = "output_single.pdf";
    const int AnnotationCount = 100;

    static void Main()
    {
        // Create a sample PDF with a number of text annotations
        CreateSamplePdf();

        // Benchmark DeleteAnnotations (deletes all annotations)
        long deleteAllTicks = BenchmarkDeleteAll();

        // Benchmark DeleteAnnotation (deletes a single annotation by name)
        long deleteSingleTicks = BenchmarkDeleteSingle();

        Console.WriteLine($"DeleteAnnotations elapsed (ticks): {deleteAllTicks}");
        Console.WriteLine($"DeleteAnnotation (single) elapsed (ticks): {deleteSingleTicks}");
    }

    static void CreateSamplePdf()
    {
        // Use using to ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Add multiple text annotations with unique names
            for (int i = 1; i <= AnnotationCount; i++)
            {
                // Define rectangle for the annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 800 - i * 5, 300, 820 - i * 5);

                // Create the annotation
                TextAnnotation annot = new TextAnnotation(page, rect)
                {
                    Title = $"Title {i}",
                    Contents = $"Annotation {i}",
                    // Assign a unique name to each annotation (used by DeleteAnnotation)
                    Name = $"Annot_{i}"
                };

                // Add the annotation to the page
                page.Annotations.Add(annot);
            }

            // Save the sample PDF
            doc.Save(SamplePdf);
        }
    }

    static long BenchmarkDeleteAll()
    {
        // Measure time for DeleteAnnotations (deletes all annotations)
        Stopwatch sw = new Stopwatch();

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(SamplePdf);
            sw.Start();
            editor.DeleteAnnotations();
            sw.Stop();
            editor.Save(OutputAll);
        }

        return sw.ElapsedTicks;
    }

    static long BenchmarkDeleteSingle()
    {
        // Measure time for DeleteAnnotation (deletes a single annotation by name)
        Stopwatch sw = new Stopwatch();

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(SamplePdf);
            // Delete the first annotation (name known from creation)
            sw.Start();
            editor.DeleteAnnotation("Annot_1");
            sw.Stop();
            editor.Save(OutputSingle);
        }

        return sw.ElapsedTicks;
    }
}