using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfFormExporter
    {
        /// <summary>
        /// Asynchronously exports the form fields of a PDF document to an XML file.
        /// The operation runs on a background thread to avoid blocking the UI thread.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file containing the form.</param>
        /// <param name="xmlPath">Path where the exported XML will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous export operation.</returns>
        public static async Task ExportFormDataToXmlAsync(string pdfPath, string xmlPath, CancellationToken cancellationToken = default)
        {
            // Validate input arguments
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("XML output path must be provided.", nameof(xmlPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("Source PDF file not found.", pdfPath);

            // Initialize the Form facade with the PDF file.
            // Form implements IDisposable, so we use a using block for deterministic cleanup.
            using (Form form = new Form(pdfPath))
            using (FileStream outputStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Run the synchronous ExportXml method on a thread‑pool thread.
                // This prevents UI thread blocking while still using the existing API.
                await Task.Run(() => form.ExportXml(outputStream), cancellationToken).ConfigureAwait(false);
            }
        }
    }

    // Minimal entry point required by the compiler for a console‑style project.
    internal class Program
    {
        // Async Main is supported from C# 7.1 onward.
        private static async Task Main(string[] args)
        {
            // Optional demonstration: if two arguments are supplied, perform the export.
            if (args.Length == 2)
            {
                await PdfFormExporter.ExportFormDataToXmlAsync(args[0], args[1]);
                Console.WriteLine("Export completed.");
            }
            else
            {
                Console.WriteLine("Usage: <pdfPath> <xmlPath>");
            }
        }
    }
}