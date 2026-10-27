using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;

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
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare a dictionary to hold form field names and their values
                var formData = new Dictionary<string, object>();

                // Ensure the document actually contains a form
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (var field in doc.Form.Fields)
                    {
                        // Some fields may have null values; handle gracefully
                        string fieldName = field?.PartialName ?? string.Empty;
                        object fieldValue = field?.Value;

                        // Store the value as a string for JSON serialization
                        formData[fieldName] = fieldValue?.ToString() ?? string.Empty;
                    }
                }

                // Serialize the dictionary to a JSON string
                string json = JsonSerializer.Serialize(formData, new JsonSerializerOptions { WriteIndented = true });

                // Convert JSON string to UTF-8 bytes
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

                // Write the JSON bytes into a GZip-compressed file
                using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
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