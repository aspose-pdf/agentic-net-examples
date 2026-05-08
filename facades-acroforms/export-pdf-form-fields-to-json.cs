using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "formData.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Export all form fields to a JSON file (indented for readability)
        using (Form form = new Form(inputPdf))
        {
            using (FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(fs, indented: true);
            }
        }

        // Verify the exported JSON structure by reading it back
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                Console.WriteLine("Exported form fields:");
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in doc.RootElement.EnumerateArray())
                    {
                        string name = element.GetProperty("Name").GetString();
                        JsonElement valueElem = element.GetProperty("Value");
                        string value = valueElem.ValueKind == JsonValueKind.String
                            ? valueElem.GetString()
                            : valueElem.ToString();

                        Console.WriteLine($"  {name}: {value}");
                    }
                }
                else
                {
                    Console.WriteLine("Unexpected JSON format – root element is not an array.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON: {ex.Message}");
        }
    }
}