using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a simple PDF document entirely in memory.
        // ------------------------------------------------------------
        using (MemoryStream inputStream = new MemoryStream())
        {
            Document sourceDoc = new Document();
            Page page = sourceDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for in‑memory editing using PdfPageEditor."));
            // Save the source document into the memory stream.
            sourceDoc.Save(inputStream);
            // Reset the position so that PdfPageEditor can read from the beginning.
            inputStream.Position = 0;

            // ------------------------------------------------------------
            // 2. Edit the PDF with PdfPageEditor using only streams.
            // ------------------------------------------------------------
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the editor to the input stream (no file path required).
                    editor.BindPdf(inputStream);

                    // Example modifications – rotate all pages 90° and set zoom to 50%.
                    editor.Rotation = 90;          // valid values: 0, 90, 180, 270
                    editor.Zoom = 0.5f;            // 1.0 = 100%

                    // ApplyChanges is optional because Save also applies them,
                    // but it makes the intent explicit.
                    editor.ApplyChanges();

                    // Save the edited PDF into the output memory stream.
                    editor.Save(outputStream);
                }

                // ------------------------------------------------------------
                // 3. (Optional) Persist the result to a file for verification.
                // ------------------------------------------------------------
                File.WriteAllBytes("output.pdf", outputStream.ToArray());
            }
        }
    }
}
