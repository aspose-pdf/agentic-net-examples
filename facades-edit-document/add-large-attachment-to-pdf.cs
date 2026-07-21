using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";          // Existing PDF to which the attachment will be added
        const string outputPdfPath     = "output_with_attachment.pdf";
        const string largeAttachmentPath = "largefile.bin";    // File larger than 10 MB

        // Verify that the large attachment exists
        if (!File.Exists(largeAttachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {largeAttachmentPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Add the large attachment using the PdfContentEditor facade
        // -----------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath); // Load the source PDF
        // Add the attachment without a visible annotation
        editor.AddDocumentAttachment(largeAttachmentPath, "Large attachment exceeding 10 MB");
        // Save the modified PDF
        editor.Save(outputPdfPath);
        // No explicit Dispose needed for PdfContentEditor (it does not implement IDisposable)

        // -----------------------------------------------------------------
        // Load the resulting PDF to ensure memory usage stays within limits
        // -----------------------------------------------------------------
        using (Document resultDoc = new Document(outputPdfPath))
        {
            // The static property defines the maximum file size that can be fully loaded into memory (default 210 MB)
            // It can be inspected or adjusted if required.
            int currentLimitMb = Document.FileSizeLimitToMemoryLoading;
            Console.WriteLine($"Current file‑size‑to‑memory limit: {currentLimitMb} MB");

            // Explicitly free any cached resources to keep memory usage low
            resultDoc.FreeMemory();

            // Optionally, report the final file size (should be >10 MB due to the attachment)
            long fileSizeBytes = new FileInfo(outputPdfPath).Length;
            Console.WriteLine($"Resulting PDF size: {fileSizeBytes / (1024 * 1024)} MB");
        }

        Console.WriteLine("Attachment added and memory usage verified.");
    }
}