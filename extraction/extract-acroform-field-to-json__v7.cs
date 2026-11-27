using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "FormG.pdf";
        const string outputPath = "FormG_CustomerName.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the AcroForm field named "CustomerName"
            // The field is expected to be a text box; cast accordingly
            var field = doc.Form["CustomerName"] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine("Field 'CustomerName' not found or is not a text box.");
                return;
            }

            string value = field.Value;

            // Serialize the field value to JSON
            var json = JsonSerializer.Serialize(
                new { CustomerName = value },
                new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"CustomerName saved to {outputPath}");
    }
}
