using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF with form fields
        const string jsonOutputPath = "formdata.txt"; // text file to store JSON

        // Verify the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: create & load)
        using (Document doc = new Document(pdfPath))
        {
            // Prepare a memory stream to receive the JSON output
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Optional: configure export options (e.g., indented output)
                ExportFieldsToJsonOptions exportOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true
                };

                // Export all form fields to JSON and write into the stream
                doc.Form.ExportToJson(jsonStream, exportOptions);

                // Reset stream position to read the generated JSON
                jsonStream.Position = 0;
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();

                    // Save the JSON string to a plain text file
                    File.WriteAllText(jsonOutputPath, json);
                }
            }
        }

        Console.WriteLine($"Form data exported to '{jsonOutputPath}'.");
    }
}