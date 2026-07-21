using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    class PdfAttachmentHelper
    {
        /// <summary>
        /// Asynchronously adds a file attachment to a PDF and saves the result.
        /// The operation runs on a background thread so the UI thread remains responsive.
        /// </summary>
        /// <param name="sourcePdfPath">Path to the source PDF file.</param>
        /// <param name="attachmentPath">Path to the file to attach.</param>
        /// <param name="attachmentDescription">Description for the attachment.</param>
        /// <param name="outputPdfPath">Path where the updated PDF will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task AddAttachmentAndSaveAsync(
            string sourcePdfPath,
            string attachmentPath,
            string attachmentDescription,
            string outputPdfPath,
            CancellationToken cancellationToken = default)
        {
            // Validate input files early to avoid unnecessary work.
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");

            if (!File.Exists(attachmentPath))
                throw new FileNotFoundException($"Attachment file not found: {attachmentPath}");

            // Run the Facade operations on a thread‑pool thread.
            await Task.Run(() =>
            {
                var editor = new PdfContentEditor();
                editor.BindPdf(sourcePdfPath);
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
                editor.Save(outputPdfPath);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    class Program
    {
        // C# 7.1+ async entry point.
        static async Task Main(string[] args)
        {
            // Expected arguments: sourcePdf attachmentFile description outputPdf
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: <sourcePdf> <attachmentFile> <description> <outputPdf>");
                return;
            }

            string sourcePdf = args[0];
            string attachment = args[1];
            string description = args[2];
            string outputPdf = args[3];

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
}