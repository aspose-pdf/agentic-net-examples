using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class FormDataTransfer
{
    static void Main()
    {
        // Paths for source PDF (with filled form), target PDF (template), and output PDF
        const string sourcePdfPath = "source_filled.pdf";
        const string targetPdfPath = "target_template.pdf";
        const string outputPdfPath = "merged_output.pdf";

        // Ensure source and target files exist
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
            // ------------------------------------------------------------
            // 1. Extract form data from the source PDF as JSON
            // ------------------------------------------------------------
            string jsonData;
            using (var sourceDoc = new Document(sourcePdfPath))
            {
                // Export form fields to a memory stream in JSON format
                using (var jsonStream = new MemoryStream())
                {
                    // ExportFieldsToJsonOptions can be omitted for default behavior
                    sourceDoc.Form.ExportToJson(jsonStream);
                    jsonStream.Position = 0;
                    using (var reader = new StreamReader(jsonStream, Encoding.UTF8))
                    {
                        jsonData = reader.ReadToEnd();
                    }
                }
            }

            // ------------------------------------------------------------
            // 2. Transform the JSON to match the target schema
            //    (example: rename a field and modify a value)
            // ------------------------------------------------------------
            // Simple transformation using string operations.
            // In real scenarios, parse the JSON and manipulate the object model.
            string transformedJson = jsonData
                .Replace("\"OldFieldName\"", "\"NewFieldName\"")          // rename field
                .Replace("\"OldValue\"", "\"NewValue\"");               // change a value

            // ------------------------------------------------------------
            // 3. Import the transformed data into the target PDF
            // ------------------------------------------------------------
            using (var targetDoc = new Document(targetPdfPath))
            {
                // Import the JSON data from a memory stream
                using (var importStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson)))
                {
                    targetDoc.Form.ImportFromJson(importStream);
                }

                // Save the updated PDF
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data transferred and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}