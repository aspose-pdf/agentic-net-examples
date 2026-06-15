using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static async Task Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string outputPdfPath   = "filled.pdf";    // Resulting PDF
        const string apiEndpoint     = "https://example.com/api/formdata"; // REST API returning JSON

        // Verify that the template PDF exists
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        // Create an HttpClient to fetch the JSON payload
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Request JSON data from the REST API
                using (HttpResponseMessage response = await httpClient.GetAsync(apiEndpoint))
                {
                    response.EnsureSuccessStatusCode();

                    // Obtain the response content as a stream (no need to load whole string into memory)
                    using (Stream jsonStream = await response.Content.ReadAsStreamAsync())
                    {
                        // Load the PDF document inside a using block for deterministic disposal
                        using (Document pdfDocument = new Document(pdfTemplatePath))
                        {
                            // Import form field values from the JSON stream
                            pdfDocument.Form.ImportFromJson(jsonStream);

                            // Save the populated PDF
                            pdfDocument.Save(outputPdfPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors (network, JSON format, PDF processing, etc.)
                Console.Error.WriteLine($"Error during import: {ex.Message}");
            }
        }

        Console.WriteLine($"Form data imported and PDF saved to '{outputPdfPath}'.");
    }
}