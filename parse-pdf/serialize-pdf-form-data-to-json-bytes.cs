using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

namespace PdfFormExtractorApp
{
    public class FormDataExtractor
    {
        /// <summary>
        /// Loads a PDF, extracts all form fields (name/value) and returns the data
        /// serialized as a JSON byte array suitable for API responses.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing JSON representation of the form data.</returns>
        public static byte[] ExtractFormData(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF not found: {pdfPath}");

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Collect form field names and their values.
                var formData = new Dictionary<string, string>();

                // The Form object may be null if the PDF has no interactive forms.
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (var field in doc.Form.Fields)
                    {
                        // Most field types expose a 'Value' property as string.
                        // Use ToString() as a fallback for non‑string values.
                        string value = field.Value?.ToString() ?? string.Empty;
                        formData[field.FullName] = value;
                    }
                }

                // Serialize the dictionary to JSON using System.Text.Json.
                // The serializer writes directly into a MemoryStream.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Write UTF‑8 JSON bytes to the stream.
                    JsonSerializer.Serialize(memoryStream, formData);
                    // Ensure all data is flushed.
                    memoryStream.Flush();

                    // Return the underlying byte array.
                    return memoryStream.ToArray();
                }
            }
        }
    }

    // Minimal entry point required by the compiler.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Optional demonstration: if a PDF path is supplied, output the JSON.
            if (args.Length > 0 && File.Exists(args[0]))
            {
                byte[] jsonBytes = FormDataExtractor.ExtractFormData(args[0]);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(jsonBytes));
            }
        }
    }
}