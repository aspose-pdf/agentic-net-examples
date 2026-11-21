using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point – async to allow awaiting the REST call
    static async Task Main(string[] args)
    {
        const string pdfPath = "template_form.pdf";   // PDF with AcroForm fields
        const string outputPath = "filled_form.pdf";
        const string apiEndpoint = "https://example.com/api/formdata"; // REST API returning JSON

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Retrieve JSON data from the REST API
        string jsonData;
        try
        {
            using var client = new HttpClient();
            jsonData = await client.GetStringAsync(apiEndpoint);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching JSON from API: {ex.Message}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using var doc = new Document(pdfPath);

        // Convert the JSON string to a UTF‑8 memory stream – ImportFromJson expects a Stream
        using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));

        // Import the JSON data into the PDF form fields (core API)
        doc.Form.ImportFromJson(jsonStream);

        // Optionally flatten the form to make fields non‑editable
        // doc.Form.Flatten();

        // Save the populated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}