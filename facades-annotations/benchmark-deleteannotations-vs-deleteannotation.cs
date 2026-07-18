using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationDeletionBenchmark
{
    // Path to the original PDF that will be used as a template
    private const string TemplatePdfPath = "template.pdf";

    // Number of annotations to add for the benchmark
    private const int AnnotationCount = 100;

    static void Main()
    {
        // Ensure the template PDF exists (create it if necessary)
        CreateTemplatePdf();

        // Benchmark DeleteAnnotations (deletes all annotations at once)
        long deleteAllTicks = BenchmarkDeleteAllAnnotations();

        // Benchmark DeleteAnnotation (deletes a single annotation by name)
        long deleteSingleTicks = BenchmarkDeleteSingleAnnotation();

        Console.WriteLine($"DeleteAnnotations (all) elapsed ticks: {deleteAllTicks}");
        Console.WriteLine($"DeleteAnnotation (single) elapsed ticks: {deleteSingleTicks}");
    }

    // Creates a PDF with a single page and a set of text annotations.
    // Each annotation is given a unique name so it can be deleted individually.
    private static void CreateTemplatePdf()
    {
        if (File.Exists(TemplatePdfPath))
            return; // Template already exists

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Prepare a list to hold the generated annotation names (optional)
            List<string> annotationNames = new List<string>();

            // Add the requested number of text annotations
            for (int i = 0; i < AnnotationCount; i++)
            {
                // Position each annotation slightly offset to avoid overlap
                double llx = 100;
                double lly = 700 - i * 5;
                double urx = 200;
                double ury = 720 - i * 5;
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                TextAnnotation annot = new TextAnnotation(page, rect)
                {
                    Title = $"Note {i}",
                    Contents = $"Annotation #{i}",
                    Color = Aspose.Pdf.Color.Yellow
                };

                // Assign a unique name to the annotation (used by DeleteAnnotation)
                string name = Guid.NewGuid().ToString();
                annot.Name = name;
                annotationNames.Add(name);

                page.Annotations.Add(annot);
            }

            // Save the template PDF
            doc.Save(TemplatePdfPath);
        }
    }

    // Measures the time required to delete all annotations using DeleteAnnotations().
    private static long BenchmarkDeleteAllAnnotations()
    {
        // Work on a copy of the template to keep the original unchanged
        string testFile = "test_delete_all.pdf";
        File.Copy(TemplatePdfPath, testFile, true);

        Stopwatch sw = new Stopwatch();

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(testFile);

            sw.Start();
            editor.DeleteAnnotations(); // Deletes all annotations in one call
            sw.Stop();

            editor.Save("output_delete_all.pdf");
        }

        // Clean up the test file
        File.Delete(testFile);
        return sw.ElapsedTicks;
    }

    // Measures the time required to delete a single annotation using DeleteAnnotation(string).
    private static long BenchmarkDeleteSingleAnnotation()
    {
        // Work on a fresh copy of the template
        string testFile = "test_delete_one.pdf";
        File.Copy(TemplatePdfPath, testFile, true);

        // Retrieve the name of the first annotation from the original template
        string firstAnnotationName = GetFirstAnnotationName(TemplatePdfPath);

        Stopwatch sw = new Stopwatch();

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(testFile);

            sw.Start();
            editor.DeleteAnnotation(firstAnnotationName); // Deletes only the specified annotation
            sw.Stop();

            editor.Save("output_delete_one.pdf");
        }

        // Clean up the test file
        File.Delete(testFile);
        return sw.ElapsedTicks;
    }

    // Helper method to extract the name of the first annotation in a PDF.
    private static string GetFirstAnnotationName(string pdfPath)
    {
        using (Document doc = new Document(pdfPath))
        {
            // Assume the first page contains annotations
            Page page = doc.Pages[1];
            if (page.Annotations.Count > 0)
            {
                // Annotation collection is 1‑based indexed
                Annotation first = page.Annotations[1];
                return first.Name;
            }
        }

        throw new InvalidOperationException("No annotations found in the template PDF.");
    }
}