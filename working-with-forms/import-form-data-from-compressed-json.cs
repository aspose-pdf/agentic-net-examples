using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the compressed JSON payload and the output PDF
        const string inputPdfPath = "form_template.pdf";
        const string compressedJsonPath = "formdata.json.gz";
        const string outputPdfPath = "filled_form.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(compressedJsonPath))
        {
            Console.Error.WriteLine($"Compressed JSON not found: {compressedJsonPath}");
            return;
        }

        // Decompress the JSON payload (assumed to be GZip‑compressed)
        string jsonData;
        using (FileStream fileStream = File.OpenRead(compressedJsonPath))
        using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
        using (StreamReader reader = new StreamReader(gzipStream))
        {
            jsonData = reader.ReadToEnd();
        }

        // Load the PDF document that contains the form fields
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the AcroForm associated with the document
            Form form = doc.Form;

            // The Form class does not expose an ImportDataJson method in the core API.
            // Instead, deserialize the JSON manually and assign values to the matching fields.
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonData))
                {
                    foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
                    {
                        // Find a field with the same name (case‑sensitive)
                        var field = form?.Fields?.FirstOrDefault(f => f.Name == prop.Name);
                        if (field != null)
                        {
                            // Most field types store their value as a string; convert accordingly.
                            // For complex field types you may need additional handling.
                            field.Value = prop.Value.GetString();
                        }
                    }
                }
            }

            // Save the updated PDF with populated form data
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
