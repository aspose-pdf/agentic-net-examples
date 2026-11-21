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
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Email { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "form_template.pdf";   // PDF with form fields
        const string outputPdfPath = "filled_form.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF using the Facade Form (dispose it after use)
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
        {
            pdfForm.BindPdf(inputPdfPath);

            // Prepare the object containing form values
            FormData data = new FormData
            {
                Name = "John Doe",
                Age = 30,
                Email = "john.doe@example.com"
            };

            // Serialize the object to JSON and write it into a memory stream
            string jsonString = JsonSerializer.Serialize(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            using (MemoryStream jsonStream = new MemoryStream(jsonBytes))
            {
                // Import the JSON data into the PDF form (correct method name)
                pdfForm.ImportJson(jsonStream);
            }

            // Save the updated PDF
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
