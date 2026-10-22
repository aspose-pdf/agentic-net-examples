using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Added for direct form field access

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "FormDataParts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // NOTE: The original code used FormDataExtractor which is not available
            // in the referenced Aspose.Pdf version.  Instead we extract the form
            // fields directly via the Aspose.Pdf.Forms namespace.
            // -----------------------------------------------------------------
            var form = pdfDoc.Form;
            if (form == null || form.Fields == null || form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Build a dictionary of field name -> field value (as JsonElement compatible objects)
            var allFields = new Dictionary<string, object>(form.Fields.Count());
            foreach (var field in form.Fields)
            {
                // Most field types expose a "Value" property that can be directly used.
                // For safety we convert nulls to empty strings.
                object value = field?.Value ?? string.Empty;
                allFields[field?.FullName ?? string.Empty] = value;
            }

            // -----------------------------------------------------------------
            // Split the dictionary into batches of at most 100 fields each.
            // -----------------------------------------------------------------
            const int batchSize = 100;
            var fieldEntries = new List<KeyValuePair<string, object>>(allFields);
            int totalBatches = (fieldEntries.Count + batchSize - 1) / batchSize;

            for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
            {
                int start = batchIndex * batchSize;
                int count = Math.Min(batchSize, fieldEntries.Count - start);

                var batchDict = new Dictionary<string, object>(count);
                for (int i = 0; i < count; i++)
                {
                    var kv = fieldEntries[start + i];
                    batchDict[kv.Key] = kv.Value;
                }

                // Serialize the batch to JSON with indentation for readability
                string batchJson = JsonSerializer.Serialize(batchDict, new JsonSerializerOptions { WriteIndented = true });

                // Write the batch to a file
                string batchFilePath = Path.Combine(outputFolder, $"formdata_part{batchIndex + 1}.json");
                File.WriteAllText(batchFilePath, batchJson);
                Console.WriteLine($"Created: {batchFilePath}");
            }
        }

        Console.WriteLine("Form data export and splitting completed.");
    }
}
