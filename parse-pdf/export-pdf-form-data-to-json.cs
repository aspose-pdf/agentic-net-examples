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
                // Export all form fields to JSON (writes to the provided stream)
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position to read from the beginning
                jsonStream.Position = 0;

                // Read the JSON string from the stream
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine("Exported form data as JSON:");
                    Console.WriteLine(json);
                }
            }
        }
    }
}