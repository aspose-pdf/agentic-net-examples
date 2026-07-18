using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "edited.pdf";
        const string auditFile  = "audit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the page editor and bind the source PDF
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPdf);

                int pageCount = editor.GetPages();

                // Log original page dimensions and rotation
                using (StreamWriter log = new StreamWriter(auditFile, false))
                {
                    log.WriteLine("=== PDF Page Audit ===");
                    log.WriteLine($"Source: {inputPdf}");
                    log.WriteLine($"Pages: {pageCount}");
                    log.WriteLine("Before edits:");
                    for (int i = 1; i <= pageCount; i++)
                    {
                        // Get size (width & height) and rotation for each page
                        PageSize size = editor.GetPageSize(i);
                        int rotation = editor.GetPageRotation(i);
                        log.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}°");
                    }
                }

                // Example edit: rotate all pages by 90 degrees
                editor.Rotation = 90;          // applies to all pages unless ProcessPages is set
                editor.ApplyChanges();        // commit the changes

                // Save the edited PDF
                editor.Save(outputPdf);

                // Log page dimensions and rotation after edits
                using (StreamWriter log = new StreamWriter(auditFile, true))
                {
                    log.WriteLine("After edits:");
                    for (int i = 1; i <= pageCount; i++)
                    {
                        PageSize size = editor.GetPageSize(i);
                        int rotation = editor.GetPageRotation(i);
                        log.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}°");
                    }
                }
            }

            Console.WriteLine($"Edited PDF saved to '{outputPdf}'. Audit log written to '{auditFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}