using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";               // existing PDF
        const string attachmentPath    = "largeAttachment.bin";     // file > 10 MB
        const string outputPdfPath     = "output_with_attachment.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Optional: adjust the file‑size‑to‑memory limit if needed
            // Document.FileSizeLimitToMemoryLoading = 500; // MB
            // Document.SetDefaultFileSizeLimitToMemoryLoading();

            // Capture memory usage before the operation
            long memoryBefore = GC.GetTotalMemory(forceFullCollection: true);

            // Bind the PDF to the Facade editor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Add the large attachment (no visual annotation)
            editor.AddDocumentAttachment(attachmentPath, "Large attachment exceeding 10 MB");

            // Save the modified PDF (uses the provided save rule)
            editor.Save(outputPdfPath);

            // Release any cached resources
            doc.FreeMemory();

            // Capture memory usage after the operation
            long memoryAfter = GC.GetTotalMemory(forceFullCollection: true);

            Console.WriteLine($"Memory before: {memoryBefore / (1024 * 1024)} MB");
            Console.WriteLine($"Memory after : {memoryAfter  / (1024 * 1024)} MB");

            // Simple verification that memory increase stays within a reasonable bound (e.g., 300 MB)
            const long maxAllowedIncrease = 300L * 1024 * 1024; // 300 MB in bytes
            if (memoryAfter - memoryBefore > maxAllowedIncrease)
            {
                Console.Error.WriteLine("Warning: Memory usage increased beyond the allowed limit.");
            }
            else
            {
                Console.WriteLine("Memory usage is within acceptable limits.");
            }
        }

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}