using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "fields.json";
        const string fieldName = "CustomerName"; // the specific field to export

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export *all* form fields to a JSON string first (ExportFieldsToJsonOptions does not exist in the current API)
            using (MemoryStream tempStream = new MemoryStream())
            {
                pdfDoc.Form.ExportToJson(tempStream);
                tempStream.Position = 0;
                string fullJson = new StreamReader(tempStream).ReadToEnd();

                // Parse the JSON, keep only the requested field, and write indented output
                using (JsonDocument doc = JsonDocument.Parse(fullJson))
                {
                    JsonWriterOptions writerOptions = new JsonWriterOptions { Indented = true };
                    using (FileStream outFile = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(outFile, writerOptions))
                    {
                        jsonWriter.WriteStartArray();
                        foreach (JsonElement element in doc.RootElement.EnumerateArray())
                        {
                            if (element.TryGetProperty("Name", out JsonElement nameProp) &&
                                nameProp.GetString() == fieldName)
                            {
                                element.WriteTo(jsonWriter);
                            }
                        }
                        jsonWriter.WriteEndArray();
                    }
                }
            }
        }

        Console.WriteLine($"Field \"{fieldName}\" exported to JSON at '{outputJsonPath}'.");
    }
}
