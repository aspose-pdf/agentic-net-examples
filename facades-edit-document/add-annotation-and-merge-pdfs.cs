using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdfPath   = "first.pdf";
        const string secondPdfPath  = "second.pdf";
        const string annotatedPath  = "first_annotated.pdf";
        const string mergedPath     = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the first PDF and add a text annotation
            // -------------------------------------------------
            using (Document doc = new Document(firstPdfPath))
            {
                // Use 1‑based page indexing
                Page page = doc.Pages[1];

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a text annotation
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title    = "Note",
                    Contents = "This is an added annotation.",
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                // Add the annotation to the page
                page.Annotations.Add(annotation);

                // Save the annotated PDF to a temporary file
                doc.Save(annotatedPath);
            }

            // -------------------------------------------------
            // 2. Merge the annotated first PDF with the second PDF
            //    using the Facades PdfFileEditor (no using block needed)
            // -------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the two PDFs in the desired order
            editor.Concatenate(new[] { annotatedPath, secondPdfPath }, mergedPath);

            Console.WriteLine($"Merged PDF saved to '{mergedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Optional cleanup of the intermediate annotated file
            if (File.Exists(annotatedPath))
            {
                try { File.Delete(annotatedPath); } catch { /* ignore */ }
            }
        }
    }
}