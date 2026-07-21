using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string compressedJsonPath = "data.json.gz";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(compressedJsonPath))
        {
            Console.Error.WriteLine($"Compressed JSON not found: {compressedJsonPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Decompress JSON and import form data
                using (FileStream fileStream = new FileStream(compressedJsonPath, FileMode.Open, FileAccess.Read))
                using (GZipStream gzip = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    doc.Form.ImportFromJson(gzip);
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}