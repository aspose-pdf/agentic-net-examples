using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        string inputPdf = "input.pdf";
        string attachmentFile = "attachment.pdf";
        string outputPdf = "output.pdf";

        await AddAttachmentAsync(inputPdf, attachmentFile, outputPdf);
        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }

    private static async Task AddAttachmentAsync(string sourcePdfPath, string attachmentPath, string resultPdfPath)
    {
        // Ensure the attachment file exists – create a minimal placeholder if it does not.
        if (!File.Exists(attachmentPath))
        {
            // Create an empty PDF as a placeholder attachment.
            using var placeholderAttachment = new Document();
            placeholderAttachment.Pages.Add();
            placeholderAttachment.Save(attachmentPath);
        }

        await Task.Run(() =>
        {
            // Ensure the source PDF exists – create a minimal placeholder if missing.
            if (!File.Exists(sourcePdfPath))
            {
                using var placeholderSource = new Document();
                placeholderSource.Pages.Add(); // one empty page
                placeholderSource.Save(sourcePdfPath);
            }

            // Use a using block to guarantee disposal of the editor.
            using var editor = new PdfContentEditor();
            editor.BindPdf(sourcePdfPath);
            editor.AddDocumentAttachment(attachmentPath, "Attached file");
            editor.Save(resultPdfPath);
        });
    }
}
