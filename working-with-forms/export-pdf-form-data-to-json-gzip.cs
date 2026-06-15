using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputGzipPath = "formdata.json.gz";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare JSON export options (optional: indented output)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Export form fields to a memory stream as JSON
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export form fields (Form.ExportToJson writes JSON to the provided stream)
                doc.Form.ExportToJson(jsonStream, jsonOptions);

                // Reset stream position before reading
                jsonStream.Position = 0;

                // Create the output GZip file and compress the JSON data
                using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
                {
                    jsonStream.CopyTo(gzipStream);
                }
            }
        }

        Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
    }
}