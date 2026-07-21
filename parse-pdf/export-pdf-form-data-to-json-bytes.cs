using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfUtilities
{
    public static class PdfFormExporter
    {
        /// <summary>
        /// Extracts all form fields from the specified PDF, serializes them to JSON,
        /// and returns the JSON as a byte array.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing the JSON representation of the form data.</returns>
        public static byte[] ExportFormDataToJsonBytes(string pdfPath)
        {
            // Validate input
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Prepare a memory stream to receive the JSON output
                using (MemoryStream jsonStream = new MemoryStream())
                {
                    // Export all form fields to JSON; no special options required (null)
                    doc.Form.ExportToJson(jsonStream, null);

                    // Ensure the stream position is at the beginning before reading
                    jsonStream.Position = 0;

                    // Convert the stream content to a byte array and return it
                    return jsonStream.ToArray();
                }
            }
        }
    }

    // Minimal entry point required for a console‑application project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Optional demonstration: if a PDF path is supplied, export its form data.
            if (args.Length > 0)
            {
                try
                {
                    byte[] jsonBytes = PdfFormExporter.ExportFormDataToJsonBytes(args[0]);
                    Console.WriteLine($"Exported JSON size: {jsonBytes.Length} bytes");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}