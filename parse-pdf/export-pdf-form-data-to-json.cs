using System;
using System.IO;
using System.Text;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            string json;

            // Export all form fields to a JSON string using ExportToJson (writes to a stream)
            using (var memory = new MemoryStream())
            {
                doc.Form.ExportToJson(memory);
                memory.Position = 0; // rewind the stream
                using (var reader = new StreamReader(memory, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
            }

            // Output the JSON string (could also be saved to a file if needed)
            Console.WriteLine(json);
        }
    }
}
