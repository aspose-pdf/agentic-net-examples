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

        // Load the PDF document and bind it to PdfPageEditor
        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            int pageCount = editor.GetPages();

            // Report properties before any edits
            Console.WriteLine("=== BEFORE EDITS ===");
            for (int i = 1; i <= pageCount; i++)
            {
                PageSize size = editor.GetPageSize(i);
                int rotation = editor.GetPageRotation(i);
                float zoom = editor.Zoom; // Zoom is a float (global setting)
                Console.WriteLine($"Page {i}: Size = {size.Width} x {size.Height}, Rotation = {rotation}°, Zoom = {zoom:F2}");
            }

            // Apply desired edits
            editor.Rotation = 90;                     // Rotate all pages 90 degrees
            editor.Zoom = 1.5f;                        // Set zoom to 150% (float literal)
            editor.PageSize = new PageSize(595f, 842f); // Set output page size (A4 in points, float literals)

            // Commit the changes to the document
            editor.ApplyChanges();

            // Report properties after edits
            Console.WriteLine("\n=== AFTER EDITS ===");
            for (int i = 1; i <= pageCount; i++)
            {
                PageSize size = editor.GetPageSize(i);
                int rotation = editor.GetPageRotation(i);
                float zoom = editor.Zoom;
                Console.WriteLine($"Page {i}: Size = {size.Width} x {size.Height}, Rotation = {rotation}°, Zoom = {zoom:F2}");
            }

            // Save the edited PDF
            editor.Save(outputPath);
            Console.WriteLine($"\nEdited PDF saved to '{outputPath}'.");
        }
    }
}
