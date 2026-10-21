using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Async entry point – works for console apps (C# 7.1+)
    static async Task Main(string[] args)
    {
        const string inputPdf   = "source.pdf";
        const string outputPdf  = "source_with_attachment.pdf";
        const string attachment = "attachment.txt";
        const string description = "Sample attachment";

        // Validate files before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        try
        {
            // Run the potentially long‑running PDF work on a background thread so the UI (or console) stays responsive
            await Task.Run(() => AddAttachment(
                srcPdf: inputPdf,
                destPdf: outputPdf,
                attachmentPath: attachment,
                description: description));

            Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Adds an embedded file attachment to a PDF using Aspose.Pdf.Document.
    /// This method is synchronous; callers should invoke it via Task.Run for async behaviour.
    /// </summary>
    private static void AddAttachment(string srcPdf, string destPdf, string attachmentPath, string description)
    {
        // Load the source PDF
        var pdfDoc = new Document(srcPdf);

        // Create a FileSpecification for the attachment.
        // Use the file name (not the full path) as the specification name.
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), description);
        // Populate the file data – Aspose requires the Contents stream to be set explicitly.
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

        // Add the file specification to the document's EmbeddedFiles collection.
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF to the destination path
        pdfDoc.Save(destPdf);
    }
}
