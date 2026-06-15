using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // Form class resides here

namespace AsposePdfApi
{
    public static class FormExportHelper
    {
        /// <summary>
        /// Asynchronously exports the form fields of a PDF document to an XML file.
        /// The operation runs on a background thread to keep the UI responsive.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF containing the form.</param>
        /// <param name="xmlOutputPath">Path where the exported XML will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        public static async Task ExportFormDataToXmlAsync(
            string pdfPath,
            string xmlOutputPath,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (string.IsNullOrWhiteSpace(xmlOutputPath))
                throw new ArgumentException("Output XML path must be provided.", nameof(xmlOutputPath));

            // Ensure the PDF file exists before proceeding.
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // Create the Form facade for the specified PDF.
            // The Form class implements IDisposable, so we use a regular using block for deterministic cleanup.
            using (Form form = new Form(pdfPath))
            // Open the output stream with asynchronous options.
            await using (FileStream outputStream = new FileStream(
                xmlOutputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // The ExportXml method is synchronous; wrap it in Task.Run to avoid blocking the UI thread.
                await Task.Run(() =>
                {
                    // Respect cancellation requests before the export starts.
                    cancellationToken.ThrowIfCancellationRequested();

                    form.ExportXml(outputStream);
                }, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    // In a real library project this class can be removed or the project type changed to a class library.
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Example usage – can be removed or replaced with real UI code.
            if (args.Length == 2)
            {
                try
                {
                    await FormExportHelper.ExportFormDataToXmlAsync(args[0], args[1]);
                    Console.WriteLine("Export completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Export failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: AsposePdfApi <pdfPath> <xmlOutputPath>");
            }
        }
    }
}
