using System;
using System.IO;
using Aspose.Pdf.Facades;          // Aspose.Pdf.Facades.Form
using Newtonsoft.Json;            // Json.NET for deserialization

// Define a typed class that matches the expected JSON structure.
// Adjust property names/types to correspond to the actual form field names.
public class MyFormData
{
    public string FullName { get; set; }
    public string Email    { get; set; }
    public int    Age      { get; set; }
    // Add additional properties as needed.
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        // Ensure the PDF file exists before proceeding.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Export form fields to JSON using Aspose.Pdf.Facades.Form.
        using (Form form = new Form(inputPdfPath))
        {
            // Export JSON into a memory stream to avoid intermediate files.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // ExportJson writes the form data as JSON; 'true' enables indentation.
                form.ExportJson(jsonStream, true);

                // Reset stream position to the beginning for reading.
                jsonStream.Position = 0;

                // Read the JSON text.
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();

                    // Deserialize the JSON into the typed object using Json.NET.
                    MyFormData formData = JsonConvert.DeserializeObject<MyFormData>(json);

                    // Example processing of the deserialized data.
                    Console.WriteLine("Deserialized Form Data:");
                    Console.WriteLine($"FullName: {formData.FullName}");
                    Console.WriteLine($"Email:    {formData.Email}");
                    Console.WriteLine($"Age:      {formData.Age}");
                }
            }
        }
    }
}