using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfApi
{
    /// <summary>
    /// Provides functionality to export PDF form data as XFDF (XML) directly to a stream.
    /// The caller can write the stream to an HTTP response, a file, or any other destination.
    /// </summary>
    public class PdfFormExporter
    {
        /// <summary>
        /// Exports all form annotations (including field values) from the specified PDF file to the supplied <see cref="Stream"/>.
        /// The caller is responsible for setting the appropriate HTTP headers (e.g., Content‑Type and Content‑Disposition) if the stream
        /// represents an ASP.NET response.
        /// </summary>
        /// <param name="pdfFilePath">Full path to the source PDF file that contains the form.</param>
        /// <param name="outputStream">The destination stream – typically <c>HttpResponse.Body</c> in ASP.NET Core or <c>Response.OutputStream</c> in classic ASP.NET.</param>
        public void ExportFormDataToStream(string pdfFilePath, Stream outputStream)
        {
            if (string.IsNullOrEmpty(pdfFilePath))
                throw new ArgumentException("PDF file path must be provided.", nameof(pdfFilePath));

            if (!File.Exists(pdfFilePath))
                throw new FileNotFoundException("PDF file not found.", pdfFilePath);

            if (outputStream == null)
                throw new ArgumentNullException(nameof(outputStream));

            // Use a using block for deterministic disposal of the Document.
            using (Document doc = new Document(pdfFilePath))
            {
                // Export all form annotations (including field values) to XFDF.
                // The method writes directly to the provided stream.
                doc.ExportAnnotationsToXfdf(outputStream);
            }

            // Ensure all buffered data is written to the underlying stream.
            // Do NOT close the stream – the caller owns its lifetime.
            outputStream.Flush();
        }
    }

    // ---------------------------------------------------------------------
    // A minimal console entry point – required because the project is built
    // as an executable. The class does not interfere with library usage; it
    // simply demonstrates how the exporter can be called from a console
    // application or from unit‑tests.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // If two arguments are supplied, treat them as input PDF and output XFDF paths.
            if (args.Length >= 2)
            {
                string pdfPath = args[0];
                string xfdfPath = args[1];

                using (FileStream outStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    var exporter = new PdfFormExporter();
                    exporter.ExportFormDataToStream(pdfPath, outStream);
                }

                Console.WriteLine($"Form data exported to '{xfdfPath}'.");
            }
            else
            {
                Console.WriteLine("Usage: AsposePdfApi <pdfPath> <outputXfdfPath>");
            }
        }
    }
}