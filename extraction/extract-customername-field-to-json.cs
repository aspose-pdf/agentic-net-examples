using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormH.pdf";
        const string outputPath = "FormH_CustomerName.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the AcroForm field named "CustomerName" using the Forms API
                Field field = doc.Form.Fields.FirstOrDefault(f => f.PartialName == "CustomerName");
                if (field == null)
                {
                    Console.Error.WriteLine("AcroForm field 'CustomerName' not found.");
                    return;
                }

                // Extract the field's value as a string
                string value = field.Value?.ToString() ?? string.Empty;

                // Create an anonymous object to hold the JSON structure
                var jsonObject = new { CustomerName = value };

                // Serialize the object to formatted JSON
                string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON string to the output file
                File.WriteAllText(outputPath, json);
            }

            Console.WriteLine($"CustomerName value saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
