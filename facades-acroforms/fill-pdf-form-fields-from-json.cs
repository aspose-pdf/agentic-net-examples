using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF, JSON data and the output PDF
        const string templatePdfPath = "template.pdf";
        const string jsonDataPath    = "data.json";
        const string outputPdfPath   = "filled.pdf";

        // Ensure the input files exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load JSON data into a JsonDocument for easy enumeration
        using (FileStream jsonStream = File.OpenRead(jsonDataPath))
        using (JsonDocument jsonDoc = JsonDocument.Parse(jsonStream))
        {
            // Initialize the Form facade with the template PDF
            using (Form form = new Form(templatePdfPath))
            {
                // Iterate over each property in the root JSON object
                foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
                {
                    string fieldName  = prop.Name;                     // JSON key -> PDF field name
                    string fieldValue = prop.Value.GetString() ?? "";   // Convert value to string

                    // Fill the corresponding form field; FillField returns true if successful
                    bool filled = form.FillField(fieldName, fieldValue);
                    if (!filled)
                    {
                        Console.WriteLine($"Warning: Field \"{fieldName}\" not found in the PDF form.");
                    }
                }

                // Save the filled PDF to the desired output path
                form.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}