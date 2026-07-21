using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // MemoryStream will hold the JSON output
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Optional: configure export options (e.g., indented JSON)
                ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true
                };

                // Export all form fields to JSON written into the stream
                doc.Form.ExportToJson(jsonStream, options);

                // Reset stream position before reading
                jsonStream.Position = 0;

                // Read the JSON string from the stream
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine("Exported JSON:");
                    Console.WriteLine(json);
                }
            }
        }
    }
}