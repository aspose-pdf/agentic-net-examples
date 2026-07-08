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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // MemoryStream will receive the JSON output
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to JSON; writes directly into the stream
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position before reading
                jsonStream.Position = 0;

                // Convert the UTF‑8 bytes in the stream to a string
                string json = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                // The JSON string now contains all extracted form data
                Console.WriteLine(json);
            }
        }
    }
}