using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "checkbox1_value.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // Access the checkbox field by its name
            CheckboxField checkBox = doc.Form["checkbox1"] as CheckboxField;
            if (checkBox == null)
            {
                Console.Error.WriteLine("Checkbox field 'checkbox1' not found.");
                return;
            }

            // Get the checked state (true = checked, false = unchecked)
            bool isChecked = checkBox.Checked;

            // Create an anonymous object to hold the value
            var jsonObject = new { checkbox1 = isChecked };

            // Serialize the object to JSON with indentation
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonObject, options);

            // Write the JSON string to a file
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Checkbox value saved to '{jsonPath}'.");
    }
}