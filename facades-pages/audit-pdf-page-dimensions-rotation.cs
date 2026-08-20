using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logPath = "audit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open log file and PDF editor within using blocks for deterministic disposal
        using (StreamWriter logWriter = new StreamWriter(logPath, false))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            int pageCount = editor.GetPages();

            logWriter.WriteLine($"Audit Log - {DateTime.Now}");
            logWriter.WriteLine($"Total pages: {pageCount}");
            logWriter.WriteLine("Before edit:");

            // Log dimensions and rotation for each page before modifications
            for (int i = 1; i <= pageCount; i++)
            {
                var size = editor.GetPageSize(i);          // returns PageSize with Width/Height
                int rotation = editor.GetPageRotation(i); // rotation in degrees
                logWriter.WriteLine($"Page {i}: Width={size.Width}, Height={size.Height}, Rotation={rotation}");
            }

            // Example edit: rotate the first page by 90 degrees
            editor.Rotation = 90;                 // set desired rotation
            editor.ProcessPages = new int[] { 1 }; // apply only to page 1
            editor.ApplyChanges();                // commit changes

            logWriter.WriteLine("After edit:");

            // Log dimensions and rotation after modifications
            for (int i = 1; i <= pageCount; i++)
            {
                var sizeAfter = editor.GetPageSize(i);
                int rotationAfter = editor.GetPageRotation(i);
                logWriter.WriteLine($"Page {i}: Width={sizeAfter.Width}, Height={sizeAfter.Height}, Rotation={rotationAfter}");
            }

            // Save the edited PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processing completed. Audit log written to '{logPath}'.");
    }
}