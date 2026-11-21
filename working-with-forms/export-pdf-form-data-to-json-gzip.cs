using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputGzipPath = "formdata.json.gz";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Collect form field names and their values
                var formData = new Dictionary<string, object>();

                // Ensure the document actually contains a form
                if (pdfDoc.Form != null && pdfDoc.Form.Fields != null)
                {
                    foreach (Field field in pdfDoc.Form.Fields)
                    {
                        // Different field types expose the value via the 'Value' property
                        // For checkboxes/radio buttons the value may be a string like "On"/"Off"
                        formData[field.FullName] = field.Value;
                    }
                }

                // Serialize the dictionary to JSON
                string jsonString = JsonSerializer.Serialize(formData, new JsonSerializerOptions { WriteIndented = true });

                // Convert JSON string to UTF-8 bytes
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

                // Write compressed JSON using GZipStream
                using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
                {
                    gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
                }

                Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}