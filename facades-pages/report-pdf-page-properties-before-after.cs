using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            int pageCount = editor.GetPages();

            // Store original properties
            var originalSizes    = new PageSize[pageCount + 1]; // 1‑based indexing
            var originalRotations = new int[pageCount + 1];
            double originalZoom = editor.Zoom; // same for all pages

            for (int i = 1; i <= pageCount; i++)
            {
                originalSizes[i]      = editor.GetPageSize(i);
                originalRotations[i] = editor.GetPageRotation(i);
            }

            // Apply desired edits
            editor.Rotation = 90;                     // rotate all pages 90°
            editor.Zoom     = 0.5f;                   // 50% zoom (float literal)
            editor.PageSize = new PageSize(595f, 842f); // A4 size in points (float literals)

            editor.ApplyChanges();

            // Save the edited document
            editor.Save(outputPath);
            editor.Close();

            // Re‑bind to the edited file to read post‑edit properties
            using (PdfPageEditor postEditor = new PdfPageEditor())
            {
                postEditor.BindPdf(outputPath);

                double postZoom = postEditor.Zoom;

                Console.WriteLine("Page Property Report");
                Console.WriteLine("--------------------");
                for (int i = 1; i <= pageCount; i++)
                {
                    PageSize beforeSize = originalSizes[i];
                    int      beforeRot  = originalRotations[i];

                    PageSize afterSize = postEditor.GetPageSize(i);
                    int      afterRot  = postEditor.GetPageRotation(i);

                    Console.WriteLine($"Page {i}:");
                    Console.WriteLine($"  Size   - Before: {beforeSize.Width:F2}×{beforeSize.Height:F2} pt, After: {afterSize.Width:F2}×{afterSize.Height:F2} pt");
                    Console.WriteLine($"  Rotation - Before: {beforeRot}°, After: {afterRot}°");
                    Console.WriteLine($"  Zoom   - Before: {originalZoom:F2}, After: {postZoom:F2}");
                }
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
