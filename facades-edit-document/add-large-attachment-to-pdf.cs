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
        const string largeFile = "large_attachment.bin"; // file >10 MB

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(largeFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {largeFile}");
            return;
        }

        // Increase the limit for loading whole files into memory if needed (default 210 MB)
        Document.FileSizeLimitToMemoryLoading = 500; // MB

        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Add the large attachment using a stream to avoid loading it entirely into memory
            using (FileStream attStream = File.OpenRead(largeFile))
            {
                editor.AddDocumentAttachment(attStream, Path.GetFileName(largeFile), "Large attachment >10 MB");
            }

            // Save the modified PDF
            editor.Save(outputPdf);

            // Release cached resources and check memory usage
            doc.FreeMemory();
            long memoryUsed = GC.GetTotalMemory(forceFullCollection: true);
            Console.WriteLine($"Memory used after operation: {memoryUsed / (1024 * 1024)} MB");
        }

        Console.WriteLine($"PDF with attachment saved to '{outputPdf}'.");
    }
}