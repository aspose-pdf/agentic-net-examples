using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentPath = "large_file.bin";
        const string description = "Large attachment";

        // Verify source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF using the Facade (load rule)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Open the attachment as a sequential read stream.
        // Passing the stream directly to AddDocumentAttachment lets Aspose.Pdf read the data in chunks,
        // avoiding loading the entire file into memory.
        using (FileStream attStream = new FileStream(
            attachmentPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 81920,   // 80 KB buffer (default) – suitable for large files
            useAsync: false))
        {
            editor.AddDocumentAttachment(attStream, Path.GetFileName(attachmentPath), description);
        }

        // Save the modified PDF (save rule)
        editor.Save(outputPdf);
        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
    }
}