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
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export form fields; default options are sufficient
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position to the beginning before reading
                jsonStream.Position = 0;

                // Read the JSON string from the memory stream
                string json = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                // Output the JSON string (or use it as needed)
                Console.WriteLine(json);
            }
        }
    }
}