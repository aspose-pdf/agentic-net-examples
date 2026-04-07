using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for creating form fields

class FormDataTransformer
{
    static void Main()
    {
        // Paths for source PDF (with original form), target PDF (where data will be imported), and output PDF
        const string sourcePdfPath = "source_form.pdf";
        const string targetPdfPath = "target_form.pdf";
        const string outputPdfPath = "merged_form.pdf";

        // ---------------------------------------------------------------
        // Ensure the sample PDFs exist – create them if they are missing.
        // This makes the program self‑contained and prevents the
        // FileNotFoundException shown in the original build output.
        // ---------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
            CreateSamplePdf(sourcePdfPath, "Name");
        if (!File.Exists(targetPdfPath))
            CreateSamplePdf(targetPdfPath, "new_Name"); // target expects the transformed name

        // ----- Step 1: Load source PDF and export its form fields to JSON -----
        string originalJson;
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Export form fields to a memory stream using the correct API
            using (MemoryStream exportStream = new MemoryStream())
            {
                sourceDoc.Form.ExportToJson(exportStream);
                exportStream.Position = 0;
                using (StreamReader reader = new StreamReader(exportStream, Encoding.UTF8))
                {
                    originalJson = reader.ReadToEnd();
                }
            }
        }

        // ----- Step 2: Transform JSON to a different schema -----
        // Example transformation: prepend "new_" to every field name
        string transformedJson = TransformJsonSchema(originalJson);

        // ----- Step 3: Load target PDF and import the transformed JSON -----
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Import the transformed JSON from a memory stream using the correct API
            using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson)))
            {
                targetDoc.Form.ImportFromJson(importStream);
            }

            // ----- Step 4: Save the updated PDF -----
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data transferred and saved to '{outputPdfPath}'.");
    }

    // Creates a minimal PDF containing a single text box form field.
    private static void CreateSamplePdf(string path, string fieldName)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text box field
            TextBoxField txt = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
            {
                PartialName = fieldName,
                Value = "Sample value"
            };
            doc.Form.Add(txt, 1);

            doc.Save(path);
        }
    }

    // Transforms the original JSON schema by renaming each top‑level field.
    // This implementation now supports both object‑based and array‑based JSON
    // structures that Aspose.Pdf may emit.
    private static string TransformJsonSchema(string json)
    {
        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            using (MemoryStream stream = new MemoryStream())
            using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
            {
                JsonElement root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Object)
                {
                    // Original code – object with named properties
                    writer.WriteStartObject();
                    foreach (JsonProperty prop in root.EnumerateObject())
                    {
                        string newName = "new_" + prop.Name;
                        writer.WritePropertyName(newName);
                        prop.Value.WriteTo(writer);
                    }
                    writer.WriteEndObject();
                }
                else if (root.ValueKind == JsonValueKind.Array)
                {
                    // Aspose.Pdf often returns an array of field objects.
                    // We create a new array where each object's property names are prefixed.
                    writer.WriteStartArray();
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        if (element.ValueKind == JsonValueKind.Object)
                        {
                            writer.WriteStartObject();
                            foreach (JsonProperty prop in element.EnumerateObject())
                            {
                                string newName = "new_" + prop.Name;
                                writer.WritePropertyName(newName);
                                prop.Value.WriteTo(writer);
                            }
                            writer.WriteEndObject();
                        }
                        else
                        {
                            // If the array contains non‑object items, copy them unchanged.
                            element.WriteTo(writer);
                        }
                    }
                    writer.WriteEndArray();
                }
                else
                {
                    // For any other JSON shape, just copy the original content.
                    root.WriteTo(writer);
                }

                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
