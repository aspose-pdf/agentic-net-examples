using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationPerformanceLogger
{
    // Helper to measure and log the duration of an operation
    private static void Measure(string operationName, Action operation)
    {
        Stopwatch sw = Stopwatch.StartNew();
        operation();
        sw.Stop();
        Console.WriteLine($"{operationName} took {sw.ElapsedMilliseconds} ms");
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Example operation: delete all existing annotations
            Measure("DeleteAnnotations", () =>
            {
                // PdfAnnotationEditor still provides DeleteAnnotations, keep using it
                using (var editor = new Aspose.Pdf.Facades.PdfAnnotationEditor(doc))
                {
                    editor.DeleteAnnotations();
                }
            });

            // Example operation: create a text annotation
            Measure("CreateText", () =>
            {
                // Fully qualified rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation attached to the first page
                var textAnnot = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title    = "Note Title",
                    Subject  = "Sample annotation text",
                    Contents = "Sample annotation text",
                    Open     = true
                    // Author property does not exist in current API; omitted.
                };

                // Add the annotation to the first page (adjust page index as needed)
                doc.Pages[1].Annotations.Add(textAnnot);
            });

            // Example operation: flatten all annotations
            Measure("FlatteningAnnotations", () =>
            {
                using (var editor = new Aspose.Pdf.Facades.PdfAnnotationEditor(doc))
                {
                    editor.FlatteningAnnotations();
                }
            });

            // Save the modified document (PDF format)
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}
