using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    // Sample data class that matches the PDF form field names
    public class FormData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool SubscribeNewsletter { get; set; }
        public int Age { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath  = "form_template.pdf";   // source PDF with AcroForm fields
        const string outputPdfPath = "form_filled.pdf";     // destination PDF after import

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Create an instance of the data object and populate it
        var data = new FormData
        {
            FirstName = "John",
            LastName = "Doe",
            SubscribeNewsletter = true,
            Age = 30
        };

        // Serialize the object to JSON (UTF‑8) and write it into a memory stream
        byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions { WriteIndented = true });
        using (var jsonStream = new MemoryStream(jsonBytes))
        {
            // Initialize the Form facade with source and destination PDF files
            using (var form = new Aspose.Pdf.Facades.Form(inputPdfPath, outputPdfPath))
            {
                // Import the JSON data into the PDF form fields
                form.ImportJson(jsonStream);

                // Save the modified PDF (the destination file was specified in the constructor)
                form.Save();
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}