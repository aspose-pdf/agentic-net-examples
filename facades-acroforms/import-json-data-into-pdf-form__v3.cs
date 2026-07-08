using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input_form.pdf";
        const string outputPdfPath = "output_filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Sample object whose data will be imported into the PDF form
        var formData = new FormData
        {
            FirstName = "Alice",
            LastName  = "Smith",
            Age       = 28,
            Subscribe = true
        };

        // Serialize the object to JSON and keep it in a memory stream
        using (var jsonStream = new MemoryStream())
        {
            JsonSerializer.Serialize(jsonStream, formData);
            jsonStream.Position = 0; // rewind for reading

            // Use Aspose.Pdf.Facades.Form to import the JSON data
            using (var form = new Aspose.Pdf.Facades.Form(inputPdfPath))
            {
                form.ImportJson(jsonStream);
                form.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }

    // Example data class – properties map to form field full names
    public class FormData
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public int    Age       { get; set; }
        public bool   Subscribe { get; set; }
    }
}