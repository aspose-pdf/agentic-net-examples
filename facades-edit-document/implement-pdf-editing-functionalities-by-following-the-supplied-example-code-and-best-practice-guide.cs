using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Replace text ----------
        // PdfContentEditor works on the PDF content directly.
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPath);
            // Replace every occurrence of "OldText" with "NewText".
            contentEditor.ReplaceText("OldText", "NewText");

            // Save the modified document to a temporary file for further processing.
            string tempPath = Path.GetTempFileName();
            contentEditor.Save(tempPath);
            // No need to call Close() explicitly – the using block disposes it.

            // ---------- Add a web link ----------
            // Load the temporary PDF, create a LinkAnnotation manually and add it to the page.
            string tempPath2 = Path.GetTempFileName();
            Document docWithLink = new Document(tempPath);

            // Define the rectangle (llx, lly, urx, ury) where the link will appear.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a clickable web link that points to the specified URL.
            // The LinkAnnotation constructor requires the owning Page and the rectangle.
            LinkAnnotation link = new LinkAnnotation(docWithLink.Pages[1], linkRect)
            {
                Action = new GoToURIAction("https://example.com")
            };

            // Add the annotation to the first page (pages are 1‑based).
            docWithLink.Pages[1].Annotations.Add(link);
            // Save the document that now contains the link.
            docWithLink.Save(tempPath2);

            // ---------- Rotate first page ----------
            // PdfPageEditor allows page‑level transformations (rotation, zoom, etc.).
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(tempPath2);
                // Rotate page 1 by 90 degrees clockwise.
                pageEditor.Rotation = 90;
                pageEditor.ProcessPages = new int[] { 1 };
                pageEditor.ApplyChanges();

                // Save the final edited PDF.
                pageEditor.Save(outputPath);
            }

            // Clean up temporary files.
            File.Delete(tempPath);
            File.Delete(tempPath2);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
