using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class ExportFormDataToJson
{
    static void Main()
    {
        // Paths for input PDF and output JSON text file
        const string pdfPath = "input.pdf";
        const string jsonOutputPath = "formData.json";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Export form fields to JSON using a memory stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export all form fields to the stream (default options)
                pdfDocument.Form.ExportToJson(jsonStream);

                // Convert the stream contents to a UTF‑8 string
                string jsonString = Encoding.UTF8.GetString(jsonStream.ToArray());

                // Write the JSON string to a text file
                File.WriteAllText(jsonOutputPath, jsonString, Encoding.UTF8);

                Console.WriteLine($"Form data exported to JSON and saved at '{jsonOutputPath}'.");
            }
        }
    }
}