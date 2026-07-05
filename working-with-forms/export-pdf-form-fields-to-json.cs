using System;
using System.IO;
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
                // The ExportToJson method writes JSON to the provided stream
                doc.Form.ExportToJson(ms);

                // Reset the stream position to read from the beginning
                ms.Position = 0;

                // Read the JSON content as a string
                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine(json); // Output the JSON string
                }
            }
        }
    }
}