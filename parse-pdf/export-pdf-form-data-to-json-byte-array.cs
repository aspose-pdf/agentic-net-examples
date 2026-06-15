using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfFormSerializerApp
{
    public static class PdfFormSerializer
    {
        /// <summary>
        /// Loads a PDF, extracts its form fields as JSON into a MemoryStream,
        /// and returns the JSON content as a byte array.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing the JSON representation of the form data.</returns>
        public static byte[] SerializeFormData(string pdfPath)
        {
            // Ensure the PDF file exists before attempting to load it.
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to JSON and write them into the MemoryStream.
                doc.Form.ExportToJson(jsonStream);

                // Return the JSON payload as a byte array.
                return jsonStream.ToArray();
            }
        }
    }

    internal class Program
    {
        // Entry point required for a console executable. It simply demonstrates the
        // usage of SerializeFormData and can be removed when the project is turned
        // into a class library.
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: PdfFormSerializerApp <pdf-path>");
                return;
            }

            try
            {
                byte[] jsonBytes = PdfFormSerializer.SerializeFormData(args[0]);
                Console.WriteLine($"JSON size: {jsonBytes.Length} bytes");
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(jsonBytes));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}