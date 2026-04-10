using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace FormImportExample
{
    // Sample data class to be serialized to JSON
    public class FormData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool SubscribeNewsletter { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string templatePath = "template.pdf";   // existing PDF with form fields
            const string outputPath   = "filled_form.pdf";

            // Verify that the template PDF exists
            if (!File.Exists(templatePath))
            {
                Console.Error.WriteLine($"Template PDF not found: {templatePath}");
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

            // Serialize the object to JSON and write it into a memory stream
            using (var jsonStream = new MemoryStream())
            {
                // Serialize with System.Text.Json (no external dependencies)
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, jsonOptions);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                jsonStream.Write(jsonBytes, 0, jsonBytes.Length);
                jsonStream.Position = 0; // reset stream position for reading

                // Use Aspose.Pdf.Facades.Form to import the JSON data into the PDF form
                using (Form form = new Form(templatePath))
                {
                    // Import all field values from the JSON stream
                    form.ImportJson(jsonStream);

                    // Save the updated PDF to the desired output file
                    form.Save(outputPath);
                }
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
    }
}