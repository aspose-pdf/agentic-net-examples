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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // MemoryStream will hold the JSON output
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to JSON (writes to the provided stream)
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position to read from the beginning
                jsonStream.Position = 0;

                // Convert the stream content to a UTF-8 string
                string json = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                // Output the JSON string (could be returned, saved, etc.)
                Console.WriteLine(json);
            }
        }
    }
}