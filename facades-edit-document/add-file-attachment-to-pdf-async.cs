using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Helper that adds a file attachment to a PDF asynchronously.
    /// </summary>
    public static class PdfAttachmentHelper
    {
        /// <summary>
        /// Asynchronously adds a file attachment to a PDF and saves the result.
        /// The operation runs on a thread‑pool thread to keep the UI responsive.
        /// </summary>
        /// <param name="sourcePdfPath">Path to the source PDF.</param>
        /// <param name="attachmentFilePath">Path to the file that will be attached.</param>
        /// <param name="attachmentDescription">Description of the attachment.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public static async Task AddAttachmentAndSaveAsync(
            string sourcePdfPath,
            string attachmentFilePath,
            string attachmentDescription,
            string outputPdfPath,
            CancellationToken cancellationToken = default)
        {
            // Validate input files.
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");

            if (!File.Exists(attachmentFilePath))
                throw new FileNotFoundException($"Attachment file not found: {attachmentFilePath}");

            // Run the Aspose.Pdf.Facades operations on a background thread.
            await Task.Run(() =>
            {
                // Bind the existing PDF.
                using var editor = new PdfContentEditor();
                editor.BindPdf(sourcePdfPath);

                // Add the attachment without any visual annotation.
                editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

                // Save the modified PDF to the specified output path.
                editor.Save(outputPdfPath);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    class Program
    {
        // Entry point required for a console application.
        // Using an async Main (C# 7.1+) so we can await the helper directly.
        static async Task Main(string[] args)
        {
            // Simple argument handling – in a real UI you would get these values from UI controls.
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: AsposePdfApi <sourcePdf> <attachmentFile> <description> <outputPdf>");
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

                Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
