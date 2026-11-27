using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormF.pdf";
        const string outputPath = "FormF_CustomerName.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the AcroForm field named "CustomerName"
                var field = doc.Form["CustomerName"] as TextBoxField;
                if (field == null)
                {
                    Console.Error.WriteLine("Field 'CustomerName' not found or not a text box.");
                    return;
                }

                string value = field.Value;

                // Create a simple JSON object
                var jsonObj = new { CustomerName = value };
                string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(outputPath, json);
            }

            Console.WriteLine($"CustomerName saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}