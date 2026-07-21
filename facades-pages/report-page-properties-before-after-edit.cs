using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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

        // Collect report lines
        List<string> report = new List<string>();

        // Use PdfPageEditor facade (implements IDisposable)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            int pageCount = editor.GetPages();

            // ---------- Before edit ----------
            report.Add("=== BEFORE EDIT ===");
            for (int i = 1; i <= pageCount; i++)
            {
                PageSize size = editor.GetPageSize(i);          // page dimensions
                int rotation = editor.GetPageRotation(i);       // rotation in degrees
                double zoom = editor.Zoom;                      // global zoom factor (double)
                report.Add($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}°, Zoom={zoom}");
            }

            // ---------- Apply edits ----------
            // Rotate all pages 90 degrees
            editor.Rotation = 90;

            // Set zoom to 150% (float literal because PdfPageEditor.Zoom expects float)
            editor.Zoom = 1.5f;

            // Change page size to A4 (595 x 842 points) – use float literals as required by PageSize ctor
            editor.PageSize = new PageSize(595f, 842f);

            // Commit changes
            editor.ApplyChanges();

            // ---------- After edit ----------
            report.Add("=== AFTER EDIT ===");
            for (int i = 1; i <= pageCount; i++)
            {
                PageSize size = editor.GetPageSize(i);
                int rotation = editor.GetPageRotation(i);
                double zoom = editor.Zoom;
                report.Add($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}°, Zoom={zoom}");
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        // Output the report
        foreach (string line in report)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
