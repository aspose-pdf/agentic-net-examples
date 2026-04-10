using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "largeAttachment.bin";
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Ensure the attachment is larger than 10 MB
        long attachmentSize = new FileInfo(attachmentPath).Length;
        if (attachmentSize <= 10L * 1024 * 1024)
        {
            Console.Error.WriteLine("Attachment must be larger than 10 MB.");
            return;
        }

        // Record memory usage before the operation
        long memoryBefore = GC.GetTotalMemory(forceFullCollection: true);
        Console.WriteLine($"Memory before: {memoryBefore / (1024 * 1024)} MB");

        // Add the large attachment using PdfContentEditor (Facades API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        // Add attachment without a visible annotation
        editor.AddDocumentAttachment(attachmentPath, "Large attachment");
        editor.Save(outputPdf);
        editor.Close(); // Release resources held by the facade

        // Optionally free any cached resources from the resulting document
        using (Document doc = new Document(outputPdf))
        {
            doc.FreeMemory();
        }

        // Record memory usage after the operation
        long memoryAfter = GC.GetTotalMemory(forceFullCollection: true);
        Console.WriteLine($"Memory after: {memoryAfter / (1024 * 1024)} MB");

        // Verify that memory consumption stays within an acceptable limit (e.g., 500 MB)
        const long maxAllowedIncrease = 500L * 1024 * 1024; // 500 MB
        if (memoryAfter - memoryBefore > maxAllowedIncrease)
        {
            Console.Error.WriteLine("Memory usage exceeded the allowed limit.");
        }
        else
        {
            Console.WriteLine("Memory usage is within acceptable limits.");
        }
    }
}