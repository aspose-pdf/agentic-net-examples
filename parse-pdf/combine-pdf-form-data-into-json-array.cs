using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF files containing form data
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain a JSON array with all form data
        const string outputJson = "combined_forms.json";

        // JSON array that will hold the exported form data from each PDF
        JsonArray combinedArray = new JsonArray();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Export the form fields to a memory stream in JSON format
                using (MemoryStream jsonStream = new MemoryStream())
                {
                    doc.Form.ExportToJson(jsonStream);
                    jsonStream.Position = 0; // Reset stream for reading

                    // Parse the exported JSON
                    using (JsonDocument jsonDoc = JsonDocument.Parse(jsonStream))
                    {
                        // The ExportToJson method writes a JSON object (or array) representing the form fields.
                        // Clone the root element and add it to the combined array.
                        combinedArray.Add(jsonDoc.RootElement.Clone());
                    }
                }
            }
        }

        // Write the combined JSON array to the output file
        using (FileStream outStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
        using (Utf8JsonWriter writer = new Utf8JsonWriter(outStream, new JsonWriterOptions { Indented = true }))
        {
            combinedArray.WriteTo(writer);
        }

        Console.WriteLine($"Combined form data saved to '{outputJson}'.");
    }
}