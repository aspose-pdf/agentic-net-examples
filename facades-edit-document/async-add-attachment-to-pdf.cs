using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfAttachmentHelper
{
    // Asynchronously adds a file attachment to a PDF and saves the result.
    // This method runs the blocking Facades operations on a background thread
    // so the UI thread remains responsive.
    public static async Task AddAttachmentAndSaveAsync(
        string sourcePdfPath,
        string attachmentFilePath,
        string attachmentDescription,
        string outputPdfPath,
        CancellationToken cancellationToken = default)
    {
        // Validate input files exist
        if (!File.Exists(sourcePdfPath))
            throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");

        if (!File.Exists(attachmentFilePath))
            throw new FileNotFoundException($"Attachment file not found: {attachmentFilePath}");

        // Run the synchronous Facades API inside Task.Run to avoid blocking UI thread
        await Task.Run(() =>
        {
            // Respect cancellation request before starting heavy work
            cancellationToken.ThrowIfCancellationRequested();

            // Create the PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the existing PDF document
                editor.BindPdf(sourcePdfPath);

                // Add the attachment without any visual annotation
                editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

                // Save the modified PDF to the specified output path
                editor.Save(outputPdfPath);
            }

            // Check for cancellation after operation completes
            cancellationToken.ThrowIfCancellationRequested();
        }, cancellationToken);
    }
}

class Program
{
    // Entry point required for a console application. Async Main returns a Task.
    static async Task Main(string[] args)
    {
        // Example paths – replace with real file locations as needed.
        string sourcePdf = "input.pdf";
        string attachment = "attachment.docx";
        string description = "User guide document";
        string outputPdf = "output.pdf";

        try
        {
            await PdfAttachmentHelper.AddAttachmentAndSaveAsync(
                sourcePdf,
                attachment,
                description,
                outputPdf,
                CancellationToken.None);

            Console.WriteLine("Attachment added and PDF saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}