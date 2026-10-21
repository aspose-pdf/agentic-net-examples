using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    // Memory limit in bytes (e.g., 200 MB)
    const long MemoryLimit = 200L * 1024 * 1024;

    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string attachmentPath = "largeAttachment.bin";
        const string outputPath = "sample_with_attachment.pdf";

        // Ensure a sample PDF exists
        if (!File.Exists(pdfPath))
        {
            using (Document doc = new Document())
            {
                // Add a blank page
                doc.Pages.Add();
                doc.Save(pdfPath);
            }
        }

        // Ensure a large attachment (>10 MB) exists
        const long desiredSize = 12L * 1024 * 1024; // 12 MB
        if (!File.Exists(attachmentPath) || new FileInfo(attachmentPath).Length < desiredSize)
        {
            // Create a dummy file filled with zeros
            using (FileStream fs = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.SetLength(desiredSize);
            }
        }

        // Record memory usage before the operation
        long memoryBefore = GetCurrentProcessMemory();
        Console.WriteLine($"Memory before attachment: {memoryBefore / (1024 * 1024)} MB");

        // Add the attachment using Document (PdfFileEditor does not support AddAttachment)
        using (Document pdfDoc = new Document(pdfPath))
        {
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Large binary attachment");
            pdfDoc.EmbeddedFiles.Add(fileSpec);
            pdfDoc.Save(outputPath);
        }

        // Record memory usage after the operation
        long memoryAfter = GetCurrentProcessMemory();
        Console.WriteLine($"Memory after attachment: {memoryAfter / (1024 * 1024)} MB");

        // Verify memory usage is within the defined limit
        if (memoryAfter - memoryBefore <= MemoryLimit)
        {
            Console.WriteLine("Memory usage is within acceptable limits.");
        }
        else
        {
            Console.WriteLine("Warning: Memory usage exceeded the defined limit.");
        }

        // Optional: verify that the attachment was added
        using (Document resultDoc = new Document(outputPath))
        {
            // Attachments are stored in the Document's EmbeddedFiles collection
            int attachmentCount = resultDoc.EmbeddedFiles.Count;
            Console.WriteLine($"Number of attachments in the PDF: {attachmentCount}");
        }
    }

    // Helper to get the current process's private memory size
    static long GetCurrentProcessMemory()
    {
        Process proc = Process.GetCurrentProcess();
        proc.Refresh();
        return proc.PrivateMemorySize64;
    }
}