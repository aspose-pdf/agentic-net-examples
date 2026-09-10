using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentPath = "largefile.bin";

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Bind the existing PDF for editing using the Facade API
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // Open the large attachment as a buffered stream.
        // BufferedStream reads data in chunks (here 80 KB) instead of loading the whole file into memory.
        using (FileStream fileStream = new FileStream(attachmentPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (BufferedStream bufferedStream = new BufferedStream(fileStream, 81920))
        {
            // Add the attachment to the PDF. The stream is consumed lazily, keeping memory usage low.
            editor.AddDocumentAttachment(bufferedStream, Path.GetFileName(attachmentPath), "Large attachment");
        }

        // Save the modified PDF. No additional SaveOptions are needed for PDF output.
        editor.Save(outputPath);
        Console.WriteLine($"Attachment added and PDF saved to '{outputPath}'.");
    }
}