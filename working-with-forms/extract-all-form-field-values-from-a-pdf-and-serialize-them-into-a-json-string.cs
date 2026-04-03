using System;
using System.IO;
using System.Text;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Path to the source PDF containing form fields
        const string pdfPath = "input.pdf";

        // Ensure the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // The JSON string that will hold the exported form data
        string jsonResult;

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Use a memory stream to capture the JSON output from ExportToJson
            using (MemoryStream ms = new MemoryStream())
            {
                // Export all form fields to JSON and write into the memory stream
                // ExportToJson writes the JSON representation of the form fields.
                doc.Form.ExportToJson(ms);

                // Reset the stream position to the beginning before reading
                ms.Position = 0;

                // Read the JSON bytes and convert to a UTF-8 string
                jsonResult = new StreamReader(ms, Encoding.UTF8).ReadToEnd();
            }
        }

        // Output the JSON string (or use it as needed)
        Console.WriteLine("Exported Form Fields JSON:");
        Console.WriteLine(jsonResult);
    }
}