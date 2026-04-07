using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Export all form fields to JSON using a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Export form fields; default ExportFieldsToJsonOptions are used
                doc.Form.ExportToJson(ms);
                ms.Position = 0; // Reset stream position for reading

                // Convert the JSON bytes to a string
                string json = new StreamReader(ms, Encoding.UTF8).ReadToEnd();

                // Output the JSON string
                Console.WriteLine(json);
            }
        }
    }
}