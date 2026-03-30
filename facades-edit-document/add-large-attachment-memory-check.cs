using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string attachmentPath = "large_file.bin";
        const string outputPdf = "sample_with_attachment.pdf";

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one if missing
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a simple one‑page PDF
            using (var doc = new Document())
            {
                // Add a blank page
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // ------------------------------------------------------------
        // Create a dummy attachment >10 MB if it does not exist
        // ------------------------------------------------------------
        if (!File.Exists(attachmentPath))
        {
            using (FileStream fs = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[1024 * 1024]; // 1 MB buffer (filled with zeros)
                for (int i = 0; i < 12; i++) // 12 MB total
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
        }

        // ------------------------------------------------------------
        // Set the memory‑loading limit (in MB). Default is 210 MB.
        // ------------------------------------------------------------
        Document.FileSizeLimitToMemoryLoading = 210;

        // Record memory usage before attachment
        long memoryBefore = GC.GetTotalMemory(true);

        // Load the PDF and add the attachment using a stream (avoids loading the whole file into memory)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        using (FileStream attachmentStream = File.OpenRead(attachmentPath))
        {
            // Position of the attachment icon on the page (0,0) – top‑left corner
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 100, 100);
            editor.CreateFileAttachment(
                rect,
                "Large attachment exceeding 10 MB",
                attachmentStream,
                Path.GetFileName(attachmentPath),
                1, // page number (1‑based)
                "Paperclip");
        }

        // Save the modified PDF
        editor.Save(outputPdf);

        // Record memory usage after attachment
        long memoryAfter = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory before: {memoryBefore / (1024 * 1024)} MB");
        Console.WriteLine($"Memory after : {memoryAfter / (1024 * 1024)} MB");
    }
}
