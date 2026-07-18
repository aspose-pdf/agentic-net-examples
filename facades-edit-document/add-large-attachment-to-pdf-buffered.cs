using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentPath = "largeAttachment.bin";
        const string attachmentName = "largeAttachment.bin";
        const string description = "Large file attachment";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Verify attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Initialize the facade and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // Open the attachment with a buffered stream to read in chunks,
        // preventing the whole file from being loaded into memory at once.
        const int bufferSize = 81920; // 80 KB buffer
        using (FileStream fileStream = new FileStream(attachmentPath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
        using (BufferedStream bufferedStream = new BufferedStream(fileStream, bufferSize))
        {
            // Add the attachment using the buffered stream
            editor.AddDocumentAttachment(bufferedStream, attachmentName, description);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}