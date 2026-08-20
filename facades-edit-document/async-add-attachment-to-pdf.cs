using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

public static class PdfAttachmentHelper
{
    /// <summary>
    /// Asynchronously adds a file attachment to a PDF document and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="attachmentFilePath">Path to the file that will be attached.</param>
    /// <param name="description">Description of the attachment.</param>
    /// <param name="outputPdfPath">Path where the updated PDF will be saved.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task AddAttachmentAsync(
        string inputPdfPath,
        string attachmentFilePath,
        string description,
        string outputPdfPath,
        CancellationToken cancellationToken = default)
    {
        // Validate input parameters early to avoid runtime errors.
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(attachmentFilePath))
            throw new ArgumentException("Attachment file path must be provided.", nameof(attachmentFilePath));
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF not found.", inputPdfPath);
        if (!File.Exists(attachmentFilePath))
            throw new FileNotFoundException("Attachment file not found.", attachmentFilePath);

        // Run the blocking Facade operations on a background thread.
        await Task.Run(() =>
        {
            // Ensure the operation respects cancellation.
            cancellationToken.ThrowIfCancellationRequested();

            // Use the PdfContentEditor facade to bind, attach, and save.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the existing PDF.
                editor.BindPdf(inputPdfPath);

                // Add the attachment without any visual annotation.
                editor.AddDocumentAttachment(attachmentFilePath, description);

                // Save the modified PDF to the specified output path.
                editor.Save(outputPdfPath);
            }
        }, cancellationToken).ConfigureAwait(false);
    }
}

public class Program
{
    // Entry point required for a console application. Using async Main (C# 7.1+).
    public static async Task Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputPdfPath> <attachmentFilePath> <description> <outputPdfPath>");
            return;
        }

        string inputPdfPath = args[0];
        string attachmentFilePath = args[1];
        string description = args[2];
        string outputPdfPath = args[3];

        try
        {
            await PdfAttachmentHelper.AddAttachmentAsync(inputPdfPath, attachmentFilePath, description, outputPdfPath);
            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}