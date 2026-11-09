using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        try
        {
            await AddAttachmentAsync(inputPdf, attachmentFile, outputPdf);
            Console.WriteLine($"Attachment added successfully. Saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Asynchronous wrapper that runs the blocking Aspose.Pdf API on a background thread
    private static Task AddAttachmentAsync(string sourcePdf, string attachmentPath, string resultPdf)
    {
        return Task.Run(() =>
        {
            using var doc = new Document(sourcePdf);

            // Create a FileSpecification for the attachment. Use only the file name for the specification
            // and provide the file data via a MemoryStream.
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // Add the file specification to the EmbeddedFiles collection (Attachments property does not exist).
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF.
            doc.Save(resultPdf);
        });
    }
}
