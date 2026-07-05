using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string outputPdfPath     = "output_with_attachment.pdf"; // result PDF
        const string attachmentPath    = "large_file.bin";     // large file to attach
        const string attachmentDesc    = "Large binary attachment";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the facade editor and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfDoc);

            // Open the attachment file with a BufferedStream to read it in chunks.
            // The buffer size (e.g., 64KB) limits memory usage while still providing efficient I/O.
            const int bufferSize = 64 * 1024; // 64 KB
            using (FileStream fileStream = new FileStream(
                       attachmentPath,
                       FileMode.Open,
                       FileAccess.Read,
                       FileShare.Read,
                       bufferSize,
                       useAsync: false))
            using (BufferedStream bufferedStream = new BufferedStream(fileStream, bufferSize))
            {
                // Add the attachment using the buffered stream.
                // The attachment name is the file name; description is optional metadata.
                editor.AddDocumentAttachment(
                    bufferedStream,
                    Path.GetFileName(attachmentPath),
                    attachmentDesc);
            }

            // Save the modified PDF. No SaveOptions are needed for PDF output.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}