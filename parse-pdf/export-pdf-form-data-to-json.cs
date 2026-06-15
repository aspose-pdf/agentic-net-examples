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
            // Use a memory stream to capture the JSON output
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to JSON and write to the stream
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position before reading
                jsonStream.Position = 0;

                // Convert the JSON bytes to a string
                string json = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                // Output the JSON string
                Console.WriteLine("Exported Form Data (JSON):");
                Console.WriteLine(json);
            }
        }
    }
}