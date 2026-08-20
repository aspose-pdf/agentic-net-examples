using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportSelectedFormFields
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will contain only the selected fields
        const string outputJsonPath = "selected_fields.json";

        // List of fully‑qualified field names to export
        var fieldsToExport = new List<string>
        {
            "CustomerName",
            "OrderDate",
            "TotalAmount"
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (var document = new Document(inputPdfPath))
            {
                // Initialise the Facades Form object on the loaded document
                using (var form = new Form(document))
                {
                    // Export all form data to a memory stream first
                    using (var allJsonStream = new MemoryStream())
                    {
                        // ExportJson writes the whole form (all fields) to the stream
                        form.ExportJson(allJsonStream, indented: true);
                        allJsonStream.Position = 0; // rewind for reading

                        // Parse the exported JSON
                        var rootNode = JsonNode.Parse(allJsonStream);

                        // The JSON structure produced by Aspose.Pdf is an object where each
                        // property name corresponds to a form field name.
                        // Build a new JSON object that contains only the requested fields.
                        var filteredObject = new JsonObject();

                        if (rootNode is JsonObject rootObj)
                        {
                            foreach (string fieldName in fieldsToExport)
                            {
                                if (rootObj.TryGetPropertyValue(fieldName, out JsonNode? fieldValue))
                                {
                                    filteredObject.Add(fieldName, fieldValue);
                                }
                            }
                        }

                        // Write the filtered JSON to the final output file
                        using (var outputFile = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                        using (var writer = new Utf8JsonWriter(outputFile, new JsonWriterOptions { Indented = true }))
                        {
                            filteredObject.WriteTo(writer);
                        }

                        Console.WriteLine($"Selected fields exported to '{outputJsonPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}