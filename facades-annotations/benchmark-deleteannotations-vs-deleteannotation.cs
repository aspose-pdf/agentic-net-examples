using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationDeletionBenchmark
{
    static void Main()
    {
        // Paths for the original PDF and temporary copies used in benchmarks
        const string originalPdf = "sample_with_annotations.pdf";
        const string allDeletedPdf = "sample_all_deleted.pdf";
        const string singleDeletedPdf = "sample_single_deleted.pdf";

        // Ensure the original PDF exists
        if (!File.Exists(originalPdf))
        {
            Console.Error.WriteLine($"Input file not found: {originalPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Create a PDF with a few annotations (if it does not already contain any)
        // -----------------------------------------------------------------
        // This block is optional – it only runs when the source PDF has no annotations.
        // It creates three text annotations, each with a unique name, and saves the file.
        using (Document doc = new Document(originalPdf))
        {
            bool hasAnnotations = false;
            foreach (Page page in doc.Pages)
            {
                if (page.Annotations.Count > 0)
                {
                    hasAnnotations = true;
                    break;
                }
            }

            if (!hasAnnotations)
            {
                // Add three sample annotations on the first page
                Page firstPage = doc.Pages[1];
                for (int i = 0; i < 3; i++)
                {
                    // Create a rectangle for the annotation position
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100 + i * 20, 700 - i * 20, 200 + i * 20, 720 - i * 20);
                    TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
                    {
                        Title = $"Note {i + 1}",
                        Contents = $"Sample annotation {i + 1}",
                        Color = Aspose.Pdf.Color.Yellow,
                        Open = true
                    };
                    // Assign a unique name to the annotation (used by DeleteAnnotation)
                    txtAnn.Name = Guid.NewGuid().ToString();
                    firstPage.Annotations.Add(txtAnn);
                }

                // Save the PDF with the new annotations
                doc.Save(originalPdf);
                Console.WriteLine("Created sample annotations in the source PDF.");
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Retrieve the name of the first annotation (used for single deletion)
        // -----------------------------------------------------------------
        string firstAnnotationName;
        using (Document doc = new Document(originalPdf))
        {
            // Assume at least one annotation exists
            Annotation firstAnn = doc.Pages[1].Annotations[1];
            firstAnnotationName = firstAnn.Name;
        }

        // -----------------------------------------------------------------
        // Benchmark: DeleteAnnotations (removes all annotations)
        // -----------------------------------------------------------------
        // Copy the original PDF to a temporary file to keep the source unchanged
        File.Copy(originalPdf, allDeletedPdf, true);
        Stopwatch swAll = Stopwatch.StartNew();
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(allDeletedPdf);
            editor.DeleteAnnotations();               // Deletes every annotation in the document
            editor.Save(allDeletedPdf);
        }
        swAll.Stop();
        Console.WriteLine($"DeleteAnnotations (all) elapsed: {swAll.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // Benchmark: DeleteAnnotation (removes a single annotation by name)
        // -----------------------------------------------------------------
        File.Copy(originalPdf, singleDeletedPdf, true);
        Stopwatch swSingle = Stopwatch.StartNew();
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(singleDeletedPdf);
            editor.DeleteAnnotation(firstAnnotationName); // Deletes only the specified annotation
            editor.Save(singleDeletedPdf);
        }
        swSingle.Stop();
        Console.WriteLine($"DeleteAnnotation (single) elapsed: {swSingle.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // Output summary
        // -----------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Benchmark results:");
        Console.WriteLine($"  DeleteAnnotations (all) : {swAll.ElapsedMilliseconds} ms");
        Console.WriteLine($"  DeleteAnnotation (single) : {swSingle.ElapsedMilliseconds} ms");
    }
}