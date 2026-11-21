using System;
using System.IO;
using Aspose.Pdf;
using Newtonsoft.Json;

namespace PdfFormExport
{
    // Define a typed class that matches the expected form fields.
    public class FormData
    {
        // Initialise non‑nullable reference properties to avoid CS8618 warnings.
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        // Add additional properties as needed to reflect the PDF form fields.
    }

    class Program
    {
        static void Main()
        {
            const string pdfPath = "form.pdf";
            const string jsonOutputPath = "formData.json";

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
                return;
            }

            try
            {
                // Load the PDF document inside a using block for deterministic disposal.
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Export form data to a JSON file. The ExportToJson overload requires a file path.
                    pdfDocument.Form.ExportToJson(jsonOutputPath);
                    Console.WriteLine($"Form data exported to JSON file: {jsonOutputPath}");

                    // Read the JSON text back into a string for deserialization.
                    string json = File.ReadAllText(jsonOutputPath);

                    // Deserialize the JSON into a strongly‑typed C# object using Json.NET.
                    FormData data = JsonConvert.DeserializeObject<FormData>(json);

                    // Example processing of the deserialized data.
                    Console.WriteLine("Deserialized Form Data:");
                    Console.WriteLine($"Name : {data?.Name}");
                    Console.WriteLine($"Email: {data?.Email}");
                    Console.WriteLine($"Age  : {data?.Age}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
