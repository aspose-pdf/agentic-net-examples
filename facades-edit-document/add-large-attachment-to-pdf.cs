using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string largeAttachmentPath = "largeAttachment.bin"; // must be >10 MB
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(largeAttachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {largeAttachmentPath}");
            return;
        }

        // Record memory usage before the operation
        Process proc = Process.GetCurrentProcess();
        long memoryBefore = proc.PrivateMemorySize64;
        Console.WriteLine($"Memory before attachment: {memoryBefore / (1024 * 1024)} MB");

        // Add the attachment using the EmbeddedFiles collection (Attachments property does not exist)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment. Use a stream to avoid loading the whole file into memory at once.
            var fileSpec = new FileSpecification(Path.GetFileName(largeAttachmentPath));
            using (FileStream attachmentStream = new FileStream(largeAttachmentPath, FileMode.Open, FileAccess.Read))
            {
                fileSpec.Contents = attachmentStream; // assign the stream as the file content
                pdfDoc.EmbeddedFiles.Add(fileSpec);
                // Save while the stream is still open – Aspose.Pdf reads the stream during Save.
                pdfDoc.Save(outputPdfPath);
            }
        }

        // Record memory usage after the operation
        proc.Refresh(); // ensure latest stats
        long memoryAfter = proc.PrivateMemorySize64;
        Console.WriteLine($"Memory after attachment: {memoryAfter / (1024 * 1024)} MB");

        // Simple verification that memory increase stays within a reasonable limit (e.g., 100 MB)
        const long maxIncreaseBytes = 100L * 1024 * 1024; // 100 MB
        long increase = memoryAfter - memoryBefore;
        if (increase > maxIncreaseBytes)
        {
            Console.WriteLine($"Warning: Memory increased by {increase / (1024 * 1024)} MB, which exceeds the limit of {maxIncreaseBytes / (1024 * 1024)} MB.");
        }
        else
        {
            Console.WriteLine($"Memory increase of {increase / (1024 * 1024)} MB is within acceptable limits.");
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}
