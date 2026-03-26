using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string baseFileName = "export_part";

        // Ensure the input PDF exists – if not, create a minimal PDF with a single form field.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdfWithForm(inputPath);
            Console.WriteLine($"Sample PDF created at '{inputPath}'.");
        }

        // Load the PDF document containing the form
        using (Document document = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (document.Form == null || document.Form.Fields == null || !document.Form.Fields.Any())
            {
                Console.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            // Export all form fields to a memory stream in JSON format
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Correct API: Document.Form.ExportToJson
                document.Form.ExportToJson(jsonStream);
                jsonStream.Position = 0;

                // Read the JSON text from the stream
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string jsonText = reader.ReadToEnd();

                    // Parse the JSON array of field objects
                    using (JsonDocument documentJson = JsonDocument.Parse(jsonText))
                    {
                        JsonElement root = documentJson.RootElement;
                        List<JsonElement> fieldElements = new List<JsonElement>();
                        foreach (JsonElement element in root.EnumerateArray())
                        {
                            fieldElements.Add(element);
                        }

                        int totalFields = fieldElements.Count;
                        int partNumber = 1;
                        for (int startIndex = 0; startIndex < totalFields; startIndex += 100)
                        {
                            int count = Math.Min(100, totalFields - startIndex);
                            JsonElement[] chunk = fieldElements.Skip(startIndex).Take(count).ToArray();
                            string chunkJson = JsonSerializer.Serialize(chunk, new JsonSerializerOptions { WriteIndented = true });
                            string outputFileName = $"{baseFileName}{partNumber}.json";
                            File.WriteAllText(outputFileName, chunkJson);
                            Console.WriteLine($"Written {count} fields to '{outputFileName}'.");
                            partNumber++;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a minimal PDF file containing a single text box form field.
    /// This helper is used only when the expected input file is missing.
    /// </summary>
    private static void CreateSamplePdfWithForm(string path)
    {
        // Create a new document with one page
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Add a text box form field
        TextBoxField textBox = new TextBoxField(doc.Pages[1], new Rectangle(100, 600, 300, 650))
        {
            PartialName = "SampleField",
            Value = "Sample value"
        };
        doc.Form.Add(textBox, 1);

        // Save the document
        doc.Save(path);
    }
}
