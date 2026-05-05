using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF in memory (so we don't depend on an external file)
        // ---------------------------------------------------------------------
        using (var sourceStream = new MemoryStream())
        {
            // Create a new PDF document with a single blank page
            var tempDoc = new Document();
            tempDoc.Pages.Add();
            tempDoc.Save(sourceStream);
            sourceStream.Position = 0; // reset for reading

            // ---------------------------------------------------------------
            // 2. Open the PDF with PdfAnnotationEditor and add/modify an annotation
            // ---------------------------------------------------------------
            using (var editor = new PdfAnnotationEditor())
            {
                // Bind the in‑memory PDF (no file system access required)
                editor.BindPdf(sourceStream);

                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = editor.Document.Pages[1];

                // Define the annotation rectangle (left, bottom, right, top)
                var rect = new Rectangle(100, 500, 200, 550);

                // Create a TextAnnotation and set its Modified date to the current time
                var annot = new TextAnnotation(page, rect)
                {
                    Modified = DateTime.Now,
                    Title    = "Updated",
                    Contents = "Modified timestamp refreshed",
                    Color    = Color.Red
                };

                // Apply the annotation to page 1 (start = end = 1)
                editor.ModifyAnnotations(1, 1, annot);

                // Save the updated PDF to disk (or you could save to another stream)
                editor.Save(outputPath);
            }
        }
    }
}
