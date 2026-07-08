using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source_form.pdf";      // PDF to extract data from
        const string targetPdfPath = "target_form.pdf";      // PDF to import transformed data into
        const string outputPdfPath = "merged_output.pdf";    // Resulting PDF

        // Ensure source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        try
        {
            // 1. Load source PDF and export its form fields to JSON (in-memory)
            string exportedJson;
            using (Document sourceDoc = new Document(sourcePdfPath))
            using (MemoryStream jsonStream = new MemoryStream())
            {
                sourceDoc.Form.ExportToJson(jsonStream);
                jsonStream.Position = 0;
                using (StreamReader reader = new StreamReader(jsonStream, Encoding.UTF8))
                {
                    exportedJson = reader.ReadToEnd();
                }
            }

            // 2. Transform the JSON to a different schema / modify values
            string transformedJson = TransformJson(exportedJson);

            // 3. Load target PDF and import the transformed JSON data into its form
            using (Document targetDoc = new Document(targetPdfPath))
            using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson)))
            {
                targetDoc.Form.ImportFromJson(importStream);
                // 4. Save the updated PDF
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data extracted, transformed, and re‑imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Example transformation: rename a field and toggle a boolean value.
    // Adjust this logic to match the required target schema.
    static string TransformJson(string json)
    {
        // Parse JSON into a mutable node tree
        JsonNode rootNode = JsonNode.Parse(json);

        // The exported JSON is typically an array of objects with "FieldName" and "FieldValue"
        if (rootNode is JsonArray array)
        {
            foreach (JsonNode item in array)
            {
                if (item is JsonObject obj)
                {
                    // Rename a specific field
                    if (obj["FieldName"]?.GetValue<string>() == "OldFieldName")
                    {
                        obj["FieldName"] = "NewFieldName";
                    }

                    // Example: invert a Yes/No value
                    if (obj["FieldValue"]?.GetValue<string>() == "Yes")
                    {
                        obj["FieldValue"] = "No";
                    }
                    else if (obj["FieldValue"]?.GetValue<string>() == "No")
                    {
                        obj["FieldValue"] = "Yes";
                    }

                    // Additional custom transformations can be added here
                }
            }
        }

        // Serialize back to JSON (indented for readability)
        return rootNode.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
    }
}