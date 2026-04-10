using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logFile = "audit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF with PdfPageEditor (facade API)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the document
            editor.BindPdf(inputPdf);

            // Total number of pages (1‑based indexing)
            int pageCount = editor.GetPages();

            // Open audit log for writing
            using (StreamWriter log = new StreamWriter(logFile, false))
            {
                log.WriteLine($"Audit Log - {DateTime.Now}");
                log.WriteLine($"Source PDF: {inputPdf}");
                log.WriteLine($"Pages: {pageCount}");
                log.WriteLine();

                // ---------- BEFORE EDIT ----------
                log.WriteLine("Before Edit:");
                for (int i = 1; i <= pageCount; i++)
                {
                    // Get page size (width & height) and rotation (degrees)
                    PageSize size = editor.GetPageSize(i);
                    int rotation = editor.GetPageRotation(i);
                    log.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}");
                }

                // ---------- EDIT OPERATION ----------
                // Example edit: rotate the first page by 90 degrees
                // Use the PageRotations dictionary to set per‑page rotation
                var rotations = new Dictionary<int, int>
                {
                    { 1, 90 } // page number -> rotation in degrees
                };
                editor.PageRotations = rotations;

                // Apply the pending changes to the document
                editor.ApplyChanges();

                // ---------- AFTER EDIT ----------
                log.WriteLine();
                log.WriteLine("After Edit:");
                for (int i = 1; i <= pageCount; i++)
                {
                    PageSize sizeAfter = editor.GetPageSize(i);
                    int rotationAfter = editor.GetPageRotation(i);
                    log.WriteLine($"Page {i}: Width={sizeAfter.Width}, Height={sizeAfter.Height}, Rotation={rotationAfter}");
                }
            }

            // Save the edited PDF to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processing complete. Audit log saved to '{logFile}'. Edited PDF saved to '{outputPdf}'.");
    }
}
