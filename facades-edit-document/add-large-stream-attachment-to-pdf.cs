using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentFilePath = "large_attachment.bin";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Use PdfContentEditor (Facade) to add the attachment
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPdfPath);

            // Open the attachment as a FileStream with a moderate buffer.
            // SequentialScan hints the OS to read the file sequentially,
            // avoiding loading the whole file into memory.
            const int bufferSize = 4 * 1024 * 1024; // 4 MiB buffer
            using (FileStream attachmentStream = new FileStream(
                       attachmentFilePath,
                       FileMode.Open,
                       FileAccess.Read,
                       FileShare.Read,
                       bufferSize,
                       FileOptions.SequentialScan))
            {
                // Add the attachment. Aspose.Pdf reads from the stream on demand,
                // so memory consumption stays low even for very large files.
                editor.AddDocumentAttachment(
                    attachmentStream,
                    Path.GetFileName(attachmentFilePath), // attachment name inside PDF
                    "Large file attachment");             // description
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}