using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentPath = "large_file.bin"; // must be >10 MB

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found (must be >10 MB): {attachmentPath}");
            return;
        }

        // Optional: adjust the in‑memory loading limit if needed (default 210 MB)
        Document.FileSizeLimitToMemoryLoading = 500; // MB

        // Capture memory usage before the operation
        long memoryBefore = GC.GetTotalMemory(true);

        // Add the large attachment using PdfContentEditor (Facades API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        // Add attachment without a visible annotation
        editor.AddDocumentAttachment(attachmentPath, "Large attachment");
        editor.Save(outputPdf);
        // No explicit Close needed; let GC handle the facade

        // Capture memory usage after the operation
        long memoryAfter = GC.GetTotalMemory(true);
        long memoryUsedMb = (memoryAfter - memoryBefore) / (1024 * 1024);
        Console.WriteLine($"Memory used for attachment operation: {memoryUsedMb} MB");

        // Verify memory usage stays within an acceptable limit (e.g., 200 MB)
        const long memoryLimitMb = 200;
        if (memoryUsedMb > memoryLimitMb)
        {
            Console.WriteLine("Warning: Memory usage exceeded the defined limit.");
        }
        else
        {
            Console.WriteLine("Memory usage is within the acceptable limit.");
        }
    }
}