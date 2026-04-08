using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Prepare a memory stream to capture the JSON output
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to JSON; no special options required
                // This uses the Form.ExportToJson(Stream) method as defined in the API
                doc.Form.ExportToJson(jsonStream);

                // Reset the stream position before reading
                jsonStream.Position = 0;

                // Read the JSON string from the memory stream
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();

                    // Output the JSON string (or use it as needed)
                    Console.WriteLine("Exported Form Data (JSON):");
                    Console.WriteLine(json);
                }
            }
        }
    }
}