using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF with form fields
        const string tempJsonPath = "formdata_full.json";      // temporary full JSON export
        const string outputDir    = "FormDataChunks";          // folder for split files
        const int   maxFieldsPerFile = 100;                    // limit per JSON file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF and export all form data to a single JSON file.
        // In recent Aspose.PDF versions the FormDataExtractor class is obsolete;
        // the Document.Form object provides ExportToJson directly.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export the entire form data to JSON.
            pdfDoc.Form.ExportToJson(tempJsonPath);
        }

        // Read the generated JSON and parse it into a JsonObject
        string fullJson = File.ReadAllText(tempJsonPath);
        JsonObject allFields = JsonNode.Parse(fullJson).AsObject();

        // Split the fields into chunks of at most maxFieldsPerFile
        int fileIndex = 0;
        int fieldCount = 0;
        JsonObject chunk = new JsonObject();

        foreach (KeyValuePair<string, JsonNode?> kvp in allFields)
        {
            // Add current field to the current chunk
            chunk.Add(kvp.Key, kvp.Value);
            fieldCount++;

            // When the chunk reaches the limit, write it out and start a new one
            if (fieldCount == maxFieldsPerFile)
            {
                fileIndex++;
                string chunkPath = Path.Combine(outputDir, $"formdata_{fileIndex}.json");
                File.WriteAllText(chunkPath, chunk.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
                // Reset for next chunk
                chunk = new JsonObject();
                fieldCount = 0;
            }
        }

        // Write any remaining fields that didn't fill a complete chunk
        if (fieldCount > 0)
        {
            fileIndex++;
            string chunkPath = Path.Combine(outputDir, $"formdata_{fileIndex}.json");
            File.WriteAllText(chunkPath, chunk.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }

        // Optionally delete the temporary full JSON file
        File.Delete(tempJsonPath);

        Console.WriteLine($"Form data exported and split into {fileIndex} file(s) under '{outputDir}'.");
    }
}
