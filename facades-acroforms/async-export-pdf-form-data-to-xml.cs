using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfDemo
{
    public static class FormExportHelper
    {
        /// <summary>
        /// Asynchronously exports the form fields of a PDF document to an XML file.
        /// The operation runs on a background thread to avoid blocking the UI thread.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file containing the form.</param>
        /// <param name="xmlPath">Path where the exported XML will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        public static async Task ExportFormDataToXmlAsync(string pdfPath, string xmlPath, CancellationToken cancellationToken = default)
        {
            // Validate input arguments
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("XML path must be provided.", nameof(xmlPath));

            // Ensure the PDF file exists before proceeding
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("Source PDF file not found.", pdfPath);

            // Form implements IDisposable, so we wrap it in a using block.
            using (Form form = new Form(pdfPath))
            {
                // Open the output XML file stream with async I/O enabled.
                using (FileStream outputStream = new FileStream(
                    xmlPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    // ExportXml is synchronous; run it on a thread‑pool thread.
                    await Task.Run(() => form.ExportXml(outputStream), cancellationToken)
                              .ConfigureAwait(false);
                }
            }
        }
    }

    internal class Program
    {
        // C# 7.1+ async entry point – satisfies the compiler's requirement for a Main method.
        private static async Task Main(string[] args)
        {
            // Example usage – replace with real file paths or pass them via command‑line arguments.
            string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";
            string xmlPath = args.Length > 1 ? args[1] : "output.xml";

            try
            {
                await FormExportHelper.ExportFormDataToXmlAsync(pdfPath, xmlPath);
                Console.WriteLine($"Form data successfully exported to '{xmlPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}