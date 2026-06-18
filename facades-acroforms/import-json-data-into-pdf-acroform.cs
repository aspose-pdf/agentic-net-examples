using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    // Sample data class to be serialized to JSON
    public class FormData
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Subscribe { get; set; }
        // Add more properties matching the PDF form field names
    }

    static void Main()
    {
        const string inputPdfPath  = "input_form.pdf";   // PDF with AcroForm fields
        const string outputPdfPath = "filled_form.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // 1. Create a Form facade bound to the source PDF
        Form form = new Form(inputPdfPath);

        // 2. Prepare the object that holds the data to import
        FormData data = new FormData
        {
            FullName = "John Doe",
            Email    = "john.doe@example.com",
            Subscribe = true
        };

        // 3. Serialize the object to JSON and write it into a memory stream
        using (MemoryStream jsonStream = new MemoryStream())
        {
            // Serialize with System.Text.Json (you can use any JSON serializer)
            string jsonString = JsonSerializer.Serialize(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            jsonStream.Write(jsonBytes, 0, jsonBytes.Length);
            jsonStream.Position = 0; // Reset stream position for reading

            // 4. Import the JSON data into the PDF form fields
            form.ImportJson(jsonStream);
        }

        // 5. Save the modified PDF to the desired output file
        form.Save(outputPdfPath);

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}