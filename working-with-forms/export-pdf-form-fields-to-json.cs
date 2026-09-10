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
            // Export form fields to a memory stream as JSON
            using (MemoryStream jsonStream = new MemoryStream())
            {
                doc.Form.ExportToJson(jsonStream);
                jsonStream.Position = 0; // Reset stream position for reading

                // Read the JSON string from the stream
                string json = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                // Output the JSON string
                Console.WriteLine(json);
            }
        }
    }
}