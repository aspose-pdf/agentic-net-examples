using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfFormExportDemo
{
    /// <summary>
    /// Helper class that extracts AcroForm field names/values from a PDF,
    /// serialises them to JSON and returns the JSON as a byte array.
    /// </summary>
    public static class PdfFormExporter
    {
        /// <summary>
        /// Loads a PDF, extracts all form field names and values,
        /// serialises the data to JSON, writes it into a MemoryStream,
        /// and returns the resulting byte array.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing the serialized form data.</returns>
        public static byte[] ExtractFormDataToByteArray(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Collect form field data into a dictionary.
                var formData = new Dictionary<string, string>();

                // The Form object may be null if the document has no interactive forms.
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Use the field's fully qualified name as the key.
                        string fieldName = field.FullName ?? field.Name ?? string.Empty;

                        // Retrieve the field value as a string (handles different field types).
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        formData[fieldName] = fieldValue;
                    }
                }

                // Serialize the dictionary to JSON.
                string json = JsonSerializer.Serialize(formData, new JsonSerializerOptions
                {
                    WriteIndented = false,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                // Write JSON into a MemoryStream and return the byte array.
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                    ms.Write(jsonBytes, 0, jsonBytes.Length);
                    // Position reset is optional because ToArray() does not depend on it.
                    return ms.ToArray();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Simple console entry point – the first argument is the PDF file path.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: PdfFormExportDemo <pdfPath>");
                return;
            }

            string pdfPath = args[0];
            try
            {
                byte[] jsonBytes = PdfFormExporter.ExtractFormDataToByteArray(pdfPath);
                Console.WriteLine($"Extracted form data size: {jsonBytes.Length} bytes");

                // Optional: write the JSON to a .json file next to the source PDF for verification.
                string outPath = Path.ChangeExtension(pdfPath, ".formdata.json");
                File.WriteAllBytes(outPath, jsonBytes);
                Console.WriteLine($"JSON saved to: {outPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
