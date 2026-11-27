using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Collect form field names and their values into a dictionary
            var fieldData = new Dictionary<string, object>();

            // The Form collection implements IEnumerable<Field>
            foreach (Field field in doc.Form)
            {
                // Some fields may have null values; handle gracefully
                object value = field.Value ?? string.Empty;
                fieldData[field.FullName] = value;
            }

            // Serialize the dictionary to JSON (UTF-8)
            string jsonString = JsonSerializer.Serialize(fieldData, new JsonSerializerOptions { WriteIndented = true });
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

            // Write JSON to a MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(jsonBytes, 0, jsonBytes.Length);
                ms.Position = 0; // Reset position before copying

                // Save the MemoryStream directly to a .json file
                using (FileStream file = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"Form fields exported to JSON file: {outputJsonPath}");
    }
}