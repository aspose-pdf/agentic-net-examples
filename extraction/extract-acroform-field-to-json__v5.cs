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
        const string inputPath = "FormE.pdf";
        const string outputPath = "FormE_CustomerName.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document within a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the AcroForm field named "CustomerName" via the Fields collection
                Field field = doc.Form.Fields
                                   .FirstOrDefault(f => f.PartialName == "CustomerName");

                if (field == null)
                {
                    Console.Error.WriteLine("AcroForm field 'CustomerName' not found.");
                    return;
                }

                // Extract the field's value as a string
                string value = field.Value?.ToString() ?? string.Empty;

                // Prepare a simple JSON object
                var jsonObject = new { CustomerName = value };
                string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"CustomerName saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
