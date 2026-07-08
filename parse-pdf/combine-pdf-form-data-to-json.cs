using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // Input PDF files containing form fields
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output file that will contain a JSON array with all form data
        string outputJson = "combined_forms.json";

        // List to hold each PDF's exported JSON as a JsonElement
        var combined = new List<JsonElement>();

        foreach (var pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: using ensures disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export the form fields to a memory stream in JSON format
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Form.ExportToJson(ms);
                    ms.Position = 0; // Reset stream for reading

                    // Parse the exported JSON and add its root element to the list
                    using (JsonDocument jsonDoc = JsonDocument.Parse(ms))
                    {
                        // Clone the root element because JsonDocument will be disposed
                        combined.Add(jsonDoc.RootElement.Clone());
                    }
                }
            }
        }

        // Write the combined JSON array to the output file
        using (FileStream outStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
        using (Utf8JsonWriter writer = new Utf8JsonWriter(outStream, new JsonWriterOptions { Indented = true }))
        {
            writer.WriteStartArray();
            foreach (var element in combined)
            {
                element.WriteTo(writer);
            }
            writer.WriteEndArray();
        }

        Console.WriteLine($"Combined JSON saved to '{outputJson}'.");
    }
}