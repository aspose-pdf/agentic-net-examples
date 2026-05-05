using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "template.pdf";
        const string outputPdfPath = "filled.pdf";
        const string jsonDataPath  = "data.json";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        try
        {
            // Load the PDF form using the Facades Form class
            using (Form form = new Form(inputPdfPath))
            {
                // Read JSON mapping field names -> values
                string jsonContent = File.ReadAllText(jsonDataPath);
                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonContent))
                {
                    foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
                    {
                        // Each property name is the full field name in the PDF
                        // Each property value is the string to fill into the field
                        string fieldName  = prop.Name;
                        string fieldValue = prop.Value.GetString() ?? string.Empty;

                        // Fill the field. The FillField method returns void, so we just call it.
                        form.FillField(fieldName, fieldValue);
                    }
                }

                // Save the updated PDF
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
