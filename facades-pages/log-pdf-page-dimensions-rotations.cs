using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "edited.pdf";
        const string logFile   = "audit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure resources are disposed properly
        using (Document doc = new Document(inputPdf))
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        using (StreamWriter logWriter = new StreamWriter(logFile, false))
        {
            // Log header
            logWriter.WriteLine("=== PDF Page Audit Log ===");
            logWriter.WriteLine($"Source: {inputPdf}");
            logWriter.WriteLine($"Timestamp: {DateTime.UtcNow:u}");
            logWriter.WriteLine();

            // -----------------------------------------------------------------
            // 1. Capture and log original page dimensions and rotations
            // -----------------------------------------------------------------
            int totalPages = editor.GetPages();
            logWriter.WriteLine("Before Edit:");
            for (int pageNum = 1; pageNum <= totalPages; pageNum++)
            {
                // Get size (width & height) and rotation (in degrees)
                PageSize size = editor.GetPageSize(pageNum);
                int rotation = editor.GetPageRotation(pageNum);

                logWriter.WriteLine(
                    $"Page {pageNum}: Width={size.Width:F2} pt, Height={size.Height:F2} pt, Rotation={rotation}°");
            }

            logWriter.WriteLine();

            // -----------------------------------------------------------------
            // 2. Apply desired edits (example: rotate all pages 90° and zoom to 120%)
            // -----------------------------------------------------------------
            editor.Rotation = 90;          // rotate all pages by 90 degrees
            editor.Zoom = 1.2f;            // 120% zoom
            editor.ApplyChanges();         // commit the changes

            // -----------------------------------------------------------------
            // 3. Capture and log post‑edit page dimensions and rotations
            // -----------------------------------------------------------------
            logWriter.WriteLine("After Edit:");
            for (int pageNum = 1; pageNum <= totalPages; pageNum++)
            {
                PageSize size = editor.GetPageSize(pageNum);
                int rotation = editor.GetPageRotation(pageNum);

                logWriter.WriteLine(
                    $"Page {pageNum}: Width={size.Width:F2} pt, Height={size.Height:F2} pt, Rotation={rotation}°");
            }

            // -----------------------------------------------------------------
            // 4. Save the edited PDF
            // -----------------------------------------------------------------
            editor.Save(outputPdf);
            logWriter.WriteLine();
            logWriter.WriteLine($"Edited PDF saved to: {outputPdf}");
        }

        Console.WriteLine("Audit completed. See audit_log.txt for details.");
    }
}