using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfApi
{
    public static class FormDataSerializer
    {
        /// <summary>
        /// Loads a PDF, extracts its form fields as JSON into a memory stream,
        /// and returns the JSON content as a byte array.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing the JSON representation of the form data.</returns>
        public static byte[] SerializeFormToJsonBytes(string pdfPath)
        {
            // Ensure the PDF file exists before attempting to load it.
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export the form fields to JSON directly into the memory stream.
                doc.Form.ExportToJson(jsonStream);

                // Return the populated memory stream as a byte array.
                return jsonStream.ToArray();
            }
        }
    }

    // Minimal entry point required for a console‑type project.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Optional demo: if a PDF path is supplied, write its form JSON to console.
            if (args.Length > 0 && File.Exists(args[0]))
            {
                byte[] jsonBytes = FormDataSerializer.SerializeFormToJsonBytes(args[0]);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(jsonBytes));
            }
        }
    }
}