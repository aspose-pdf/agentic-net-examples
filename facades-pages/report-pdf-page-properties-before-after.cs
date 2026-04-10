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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load original PDF and bind it to PdfPageEditor
        using (Document originalDoc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(originalDoc);

            // Report properties before any changes
            int pageCount = editor.GetPages();
            Console.WriteLine("=== BEFORE EDIT ===");
            for (int i = 1; i <= pageCount; i++) // 1‑based indexing
            {
                // Size of the page
                PageSize size = editor.GetPageSize(i);
                // Rotation of the page (in degrees)
                int rotation = editor.GetPageRotation(i);
                // Zoom coefficient (global for the editor)
                double zoom = editor.Zoom;

                Console.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}°, Zoom={zoom}");
            }

            // ---- Apply edits ----
            // Rotate all pages by 90 degrees
            editor.Rotation = 90;
            // Set zoom to 150%
            editor.Zoom = 1.5f;               // <-- float literal
            // Change page size to A4 (595 x 842 points)
            editor.PageSize = new PageSize(595f, 842f); // <-- float literals

            // Commit the changes to the document
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);
        }

        // Load the edited PDF to report properties after edits
        using (Document editedDoc = new Document(outputPath))
        using (PdfPageEditor editedEditor = new PdfPageEditor())
        {
            editedEditor.BindPdf(editedDoc);

            int pageCount = editedEditor.GetPages();
            Console.WriteLine("\n=== AFTER EDIT ===");
            for (int i = 1; i <= pageCount; i++) // 1‑based indexing
            {
                PageSize size = editedEditor.GetPageSize(i);
                int rotation = editedEditor.GetPageRotation(i);
                double zoom = editedEditor.Zoom;

                Console.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}°, Zoom={zoom}");
            }
        }

        Console.WriteLine("\nProcessing completed.");
    }
}
