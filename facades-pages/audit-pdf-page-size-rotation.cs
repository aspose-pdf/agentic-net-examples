using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string auditLog = "audit.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open audit log for appending
        using (StreamWriter log = new StreamWriter(auditLog, append: true))
        {
            // ---------- BEFORE EDIT ----------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPdf);

                int pageCount = editor.GetPages();
                log.WriteLine($"--- Audit started at {DateTime.Now} ---");
                for (int i = 1; i <= pageCount; i++)
                {
                    // Retrieve original page size and rotation
                    PageSize size = editor.GetPageSize(i);
                    int rotation = editor.GetPageRotation(i);
                    log.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}");
                }

                // ---------- EDIT OPERATION ----------
                // Example edit: rotate all pages 90 degrees and enlarge page size by 10%
                editor.Rotation = 90; // sets rotation for all pages (0, 90, 180, 270)

                // Compute new page size based on the first page
                PageSize firstSize = editor.GetPageSize(1);
                double newWidth = firstSize.Width * 1.10;
                double newHeight = firstSize.Height * 1.10;
                // PageSize constructor expects float values → cast accordingly
                editor.PageSize = new PageSize((float)newWidth, (float)newHeight);

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF
                editor.Save(outputPdf);
            }

            // ---------- AFTER EDIT ----------
            using (PdfPageEditor postEditor = new PdfPageEditor())
            {
                postEditor.BindPdf(outputPdf);
                int pageCount = postEditor.GetPages();
                for (int i = 1; i <= pageCount; i++)
                {
                    PageSize size = postEditor.GetPageSize(i);
                    int rotation = postEditor.GetPageRotation(i);
                    log.WriteLine($"[After] Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}");
                }
                log.WriteLine($"--- Audit finished at {DateTime.Now} ---");
            }
        }

        Console.WriteLine($"Processing completed. Audit log written to '{auditLog}'.");
    }
}
