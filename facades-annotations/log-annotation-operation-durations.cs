using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationPerformanceLogger
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor facade on the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // -----------------------------------------------------------------
                // 1. Add a TextAnnotation to the first page and record the duration
                // -----------------------------------------------------------------
                Stopwatch swAdd = Stopwatch.StartNew();

                // Retrieve the first page
                Page page = doc.Pages[1];

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the TextAnnotation using the (Page, Rectangle) constructor
                TextAnnotation textAnnot = new TextAnnotation(page, rect)
                {
                    Title    = "PerformanceLog",
                    Contents = "Added by logger",
                    Open     = true,
                    Color    = Aspose.Pdf.Color.Yellow
                };

                // Add the annotation to the page's collection
                page.Annotations.Add(textAnnot);

                swAdd.Stop();
                Console.WriteLine($"Add TextAnnotation duration: {swAdd.Elapsed}");

                // -----------------------------------------------------------------
                // 2. Modify the recently added annotation (change its contents)
                // -----------------------------------------------------------------
                Stopwatch swModify = Stopwatch.StartNew();

                // Find the annotation by its Title (or by index if preferred)
                // Here we use the first annotation on the page for simplicity
                if (page.Annotations.Count > 0)
                {
                    Annotation firstAnnot = page.Annotations[1]; // 1‑based indexing
                    firstAnnot.Contents = "Modified content by logger";
                }

                swModify.Stop();
                Console.WriteLine($"Modify Annotation duration: {swModify.Elapsed}");

                // -----------------------------------------------------------------
                // 3. Delete the annotation and record the duration
                // -----------------------------------------------------------------
                Stopwatch swDelete = Stopwatch.StartNew();

                // Delete by index (1‑based). After modification the annotation is still at index 1.
                page.Annotations.Delete(1);

                swDelete.Stop();
                Console.WriteLine($"Delete Annotation duration: {swDelete.Elapsed}");

                // -----------------------------------------------------------------
                // Save the modified document (no explicit SaveOptions needed for PDF)
                // -----------------------------------------------------------------
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
    }
}