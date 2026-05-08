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
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public bool   Subscribe { get; set; }
        public int    Age       { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath  = "input_form.pdf";   // PDF with AcroForm fields
        const string outputPdfPath = "filled_form.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create an instance of the Facades Form class and bind the source PDF
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form())
        {
            form.BindPdf(inputPdfPath);   // load the PDF to be filled

            // Prepare the object that holds the form values
            var data = new FormData
            {
                FirstName = "John",
                LastName  = "Doe",
                Subscribe = true,
                Age       = 30
            };

            // Serialize the object to JSON and write it into a memory stream
            string jsonString = JsonSerializer.Serialize(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            using (MemoryStream jsonStream = new MemoryStream(jsonBytes))
            {
                // Import the JSON data into the PDF form fields
                form.ImportJson(jsonStream);
            }

            // Save the updated PDF to the output file
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}