using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;               // Document, Form, etc.

class Program
{
    // Async entry point to allow awaiting the HTTP request.
    static async Task Main()
    {
        const string pdfTemplatePath = "template.pdf";   // Existing PDF with form fields
        const string outputPdfPath   = "filled.pdf";    // Destination for the populated PDF
        const string apiEndpoint     = "https://example.com/api/formdata"; // REST API returning JSON

        // Verify that the template PDF exists before proceeding.
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document document = new Document(pdfTemplatePath))
        {
            // Create an HttpClient to request the JSON payload.
            using (HttpClient httpClient = new HttpClient())
            // Get the JSON response as a stream; the stream will be disposed automatically.
            using (Stream jsonStream = await httpClient.GetStreamAsync(apiEndpoint))
            {
                // Import form field values from the JSON stream into the PDF form.
                // ImportFromJson returns a collection of FieldSerializationResult; we ignore it here.
                document.Form.ImportFromJson(jsonStream);
            }

            // Save the updated PDF. No special SaveOptions are required for PDF output.
            document.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}