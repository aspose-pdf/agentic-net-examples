using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Export form fields to JSON using a memory stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to the stream
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position to the beginning before reading
                jsonStream.Position = 0;

                // Read the JSON content as a string
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine("Form fields JSON:");
                    Console.WriteLine(json);
                }
            }
        }
    }
}