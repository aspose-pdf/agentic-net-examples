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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Export all form fields to JSON using a memory stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // The ExportToJson method writes JSON to the provided stream
                doc.Form.ExportToJson(jsonStream);

                // Reset stream position to the beginning before reading
                jsonStream.Position = 0;

                // Convert the stream contents to a UTF‑8 string
                string json = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                // Output the JSON string
                Console.WriteLine("Exported form data as JSON:");
                Console.WriteLine(json);
            }
        }
    }
}