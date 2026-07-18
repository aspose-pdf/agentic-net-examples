using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace FormJsonImportExample
{
    // Sample data class to be serialized to JSON
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Paths to the source PDF form and the output PDF after import
            const string sourcePdfPath = "inputForm.pdf";
            const string outputPdfPath = "outputForm.pdf";

            // Ensure the source PDF form exists
            if (!File.Exists(sourcePdfPath))
            {
                Console.Error.WriteLine($"Source PDF form not found: {sourcePdfPath}");
                return;
            }

            // Create an instance of the data to be imported
            var personData = new Person
            {
                Name = "John Doe",
                Age = 42
            };

            // Serialize the object to JSON and write it into a memory stream
            using (var jsonStream = new MemoryStream())
            {
                // Serialize directly into the stream
                JsonSerializer.Serialize(jsonStream, personData);
                // Reset stream position for reading
                jsonStream.Position = 0;

                // Initialize the Form facade with source and destination files
                using (var form = new Form(sourcePdfPath, outputPdfPath))
                {
                    // Import the JSON data into the PDF form fields
                    form.ImportJson(jsonStream);

                    // Save the modified PDF to the output path
                    form.Save();
                }
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
    }
}