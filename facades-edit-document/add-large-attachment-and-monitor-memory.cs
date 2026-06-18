using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "large_attachment.bin"; // file >10 MB
        const string outputPdf = "output_with_attachment.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Capture memory usage before the operation
        long beforeMemory = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory before: {beforeMemory / (1024 * 1024)} MB");

        // Load the PDF, add the large attachment, and save
        using (Document doc = new Document(inputPdf))
        {
            // Optionally raise the in‑memory loading limit (default 210 MB)
            Document.FileSizeLimitToMemoryLoading = 500; // MB

            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);
            // Add attachment without a visible annotation
            editor.AddDocumentAttachment(attachmentPath, "Large attachment >10 MB");
            // Persist the changes
            doc.Save(outputPdf);
        }

        // Capture memory usage after the operation
        long afterMemory = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory after: {afterMemory / (1024 * 1024)} MB");
        Console.WriteLine($"Memory delta: {(afterMemory - beforeMemory) / (1024 * 1024)} MB");

        // Simple verification that memory stays within a safe threshold (e.g., 300 MB)
        const long maxAllowed = 300L * 1024 * 1024;
        if (afterMemory > maxAllowed)
        {
            Console.Error.WriteLine("Warning: Memory usage exceeded the safe limit.");
        }
        else
        {
            Console.WriteLine("Memory usage is within acceptable limits.");
        }
    }
}