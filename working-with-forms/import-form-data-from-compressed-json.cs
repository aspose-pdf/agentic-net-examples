using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF with form fields
        const string outputPdfPath = "filled.pdf";   // Resulting PDF after import
        const string compressedJsonPath = "data.json.gz"; // GZip‑compressed JSON payload

        // Verify required files exist before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(compressedJsonPath))
        {
            Console.WriteLine($"Compressed JSON file not found: {compressedJsonPath}");
            return;
        }

        // Decompress the JSON payload into a string
        string jsonData;
        using (FileStream fs = File.OpenRead(compressedJsonPath))
        using (GZipStream gzip = new GZipStream(fs, CompressionMode.Decompress))
        using (StreamReader reader = new StreamReader(gzip))
        {
            jsonData = reader.ReadToEnd();
        }

        // Load the PDF, import the form data, and save the result
        using (Document doc = new Document(pdfPath))
        {
            // Import form field values from the JSON string using the core Forms API
            using (MemoryStream jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                // Correct API call – ImportFromJson, not ImportDataJson
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
