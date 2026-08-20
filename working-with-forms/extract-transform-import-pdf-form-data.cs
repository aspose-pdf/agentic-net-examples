using System;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "sourceForm.pdf";   // PDF containing the original form data
        const string targetPdfPath = "targetForm.pdf";   // PDF that will receive the transformed data
        const string resultPdfPath = "resultForm.pdf";   // Final PDF after import

        // ------------------------------------------------------------
        // 1. Create sample PDFs so the example is self‑contained.
        // ------------------------------------------------------------
        CreateSourcePdf(sourcePdfPath);
        CreateTargetPdf(targetPdfPath);

        // ------------------------------------------------------------
        // 2. Load the source PDF and export its form fields to JSON (in memory)
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (MemoryStream exportStream = new MemoryStream())
        {
            // Export form fields to JSON – the correct Aspose.Pdf API method
            sourceDoc.Form.ExportToJson(exportStream);
            exportStream.Position = 0; // rewind for reading

            // 3. Read the exported JSON as a string
            string originalJson = new StreamReader(exportStream, Encoding.UTF8).ReadToEnd();

            // 4. Transform the JSON to a different schema (example: prefix each field name with "new_")
            string transformedJson = TransformJsonSchema(originalJson);

            // 5. Load the target PDF where the transformed data will be imported
            using (Document targetDoc = new Document(targetPdfPath))
            // 6. Import the transformed JSON back into the target PDF's form
            using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson)))
            {
                targetDoc.Form.ImportFromJson(importStream);
                // 7. Save the resulting PDF
                targetDoc.Save(resultPdfPath);
            }
        }

        Console.WriteLine("Form data extracted, transformed, and re‑imported successfully.");
    }

    // ------------------------------------------------------------
    // Helper: create a source PDF with a simple form field.
    // ------------------------------------------------------------
    private static void CreateSourcePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a text box field named "Name" with a default value.
            TextBoxField nameField = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
            {
                PartialName = "Name",
                Value = "John Doe"
            };
            doc.Form.Add(nameField, 1);
            doc.Save(path);
        }
    }

    // ------------------------------------------------------------
    // Helper: create a target PDF that expects the transformed field names.
    // ------------------------------------------------------------
    private static void CreateTargetPdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // The transformed field will be "new_Name".
            TextBoxField newNameField = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
            {
                PartialName = "new_Name"
            };
            doc.Form.Add(newNameField, 1);
            doc.Save(path);
        }
    }

    // Simple example transformation: rename each field by adding a "new_" prefix.
    // Adjust this method to implement any required schema conversion.
    static string TransformJsonSchema(string json)
    {
        JsonNode? rootNode = JsonNode.Parse(json);
        if (rootNode is JsonObject obj)
        {
            JsonObject transformed = new JsonObject();
            foreach (var kvp in obj)
            {
                string newKey = "new_" + kvp.Key;          // rename the field
                transformed[newKey] = kvp.Value;           // keep the original value
            }
            return transformed.ToJsonString();
        }
        // If the JSON structure is not an object (or parsing failed), return the original JSON.
        return json;
    }
}
