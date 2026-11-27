using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormC.pdf";
        const string outputPath = "FormC_CustomerName.json";

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
                // Retrieve the AcroForm field named "CustomerName"
                // The field is expected to be a TextBoxField; adjust the cast if needed for other field types
                TextBoxField field = doc.Form["CustomerName"] as TextBoxField;

                if (field == null)
                {
                    Console.Error.WriteLine("AcroForm field 'CustomerName' not found or is not a TextBoxField.");
                    return;
                }

                // Get the field's value
                string fieldValue = field.Value;

                // Prepare a simple JSON object with the field value
                var jsonObject = new { CustomerName = fieldValue };
                string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Field value saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}