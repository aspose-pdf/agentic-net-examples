using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfAttachmentHelper
{
    // Asynchronously adds a file attachment to a PDF and saves the result.
    // This method runs the blocking Facades operations on a background thread
    // to keep the UI responsive.
    public static async Task AddAttachmentAndSaveAsync(
        string sourcePdfPath,
        string attachmentFilePath,
        string attachmentDescription,
        string outputPdfPath,
        CancellationToken cancellationToken = default)
    {
        // Validate input paths (optional but helpful)
        if (!File.Exists(sourcePdfPath))
            throw new FileNotFoundException("Source PDF not found.", sourcePdfPath);
        if (!File.Exists(attachmentFilePath))
            throw new FileNotFoundException("Attachment file not found.", attachmentFilePath);

        // Run the Facades workflow on a thread‑pool thread.
        await Task.Run(() =>
        {
            // Respect cancellation request before starting heavy work.
            cancellationToken.ThrowIfCancellationRequested();

            // PdfContentEditor is a Facades class that provides BindPdf, AddDocumentAttachment, and Save.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the existing PDF.
                editor.BindPdf(sourcePdfPath);

                // Add the attachment without any visual annotation.
                editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

                // Save the modified PDF to the desired output location.
                editor.Save(outputPdfPath);
            }

            // Check for cancellation after the operation completes.
            cancellationToken.ThrowIfCancellationRequested();
        }, cancellationToken);
    }
}

// Minimal console entry point so the project builds as an executable.
// In a UI application you would call PdfAttachmentHelper.AddAttachmentAndSaveAsync
// from the UI thread (e.g., button click handler) and await it.
class Program
{
    // C# 7.1+ supports async Main returning Task.
    public static async Task Main(string[] args)
    {
        // Example usage – replace paths with real files when testing.
        // If no arguments are supplied we simply exit.
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <sourcePdf> <attachmentFile> <attachmentDescription> <outputPdf>");
            return;
        }

        string sourcePdf = args[0];
        string attachmentFile = args[1];
        string description = args[2];
        string outputPdf = args[3];

        try
        {
            await PdfAttachmentHelper.AddAttachmentAndSaveAsync(
                sourcePdf,
                attachmentFile,
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
