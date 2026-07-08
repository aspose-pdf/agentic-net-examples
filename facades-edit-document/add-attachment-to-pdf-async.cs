using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfDemo
{
    public static class PdfAttachmentHelper
    {
        /// <summary>
        /// Asynchronously adds a file attachment to a PDF and saves the result.
        /// The operation runs on a background thread to keep the UI responsive.
        /// </summary>
        /// <param name="sourcePdfPath">Path to the source PDF file.</param>
        /// <param name="attachmentPath">Path to the file that will be attached.</param>
        /// <param name="attachmentDescription">Description for the attachment.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task AddAttachmentAndSaveAsync(
            string sourcePdfPath,
            string attachmentPath,
            string attachmentDescription,
            string outputPdfPath,
            CancellationToken cancellationToken = default)
        {
            // Validate input files exist
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");

            if (!File.Exists(attachmentPath))
                throw new FileNotFoundException($"Attachment file not found: {attachmentPath}");

            // Run the synchronous Facade API on a background thread
            await Task.Run(() =>
            {
                var editor = new PdfContentEditor();
                editor.BindPdf(sourcePdfPath);
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
                editor.Save(outputPdfPath);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    internal class Program
    {
        // Async Main provides an entry point for the console application.
        // It can be removed when the library is referenced from another project.
        private static async Task Main(string[] args)
        {
            // Simple demo: expect three arguments – source PDF, attachment file, output PDF.
            if (args.Length == 3)
            {
                try
                {
                    await PdfAttachmentHelper.AddAttachmentAndSaveAsync(
                        sourcePdfPath: args[0],
                        attachmentPath: args[1],
                        attachmentDescription: "User attachment",
                        outputPdfPath: args[2],
                        cancellationToken: CancellationToken.None);

                    Console.WriteLine("Attachment added and PDF saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: <sourcePdfPath> <attachmentPath> <outputPdfPath>");
            }
        }
    }
}
