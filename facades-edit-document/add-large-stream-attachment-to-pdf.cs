using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";
        const string attachmentPath = "largeAttachment.bin";
        const string outputPdfPath = "output_with_attachment.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Define a buffer size (e.g., 4 MB) to limit memory usage while reading the attachment.
        const int bufferSize = 4 * 1024 * 1024;

        // Load the PDF document.
        Document pdfDoc = new Document(sourcePdfPath);

        // Open the attachment file as a buffered stream so that only chunks are kept in memory.
        using (FileStream fileStream = new FileStream(attachmentPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (BufferedStream bufferedStream = new BufferedStream(fileStream, bufferSize))
        {
            // Create a FileSpecification from the buffered stream. The name shown in the PDF is the file name.
            var fileSpec = new FileSpecification(bufferedStream, Path.GetFileName(attachmentPath), "Embedded attachment");
            // Add the file specification to the EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);
        }

        // Save the modified PDF.
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}
