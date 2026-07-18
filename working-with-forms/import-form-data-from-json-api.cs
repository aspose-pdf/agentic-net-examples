using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Entry point – async to allow awaiting HTTP calls
    static async Task Main()
    {
        const string inputPdfPath  = "template.pdf";          // PDF with form fields
        const string outputPdfPath = "filled.pdf";            // Resulting PDF
        const string jsonApiUrl    = "https://example.com/api/formdata"; // REST endpoint returning JSON

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Create a single HttpClient instance (best practice)
        using HttpClient httpClient = new HttpClient();

        try
        {
            // Fetch JSON data from the REST API
            using HttpResponseMessage response = await httpClient.GetAsync(jsonApiUrl);
            response.EnsureSuccessStatusCode();

            // Obtain the response content as a stream
            await using Stream jsonStream = await response.Content.ReadAsStreamAsync();

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import form field values from the JSON stream
                // ImportFromJson returns a collection of results; we ignore it here
                pdfDoc.Form.ImportFromJson(jsonStream);

                // Save the populated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (HttpRequestException httpEx)
        {
            Console.Error.WriteLine($"HTTP request failed: {httpEx.Message}");
        }
        catch (IOException ioEx)
        {
            Console.Error.WriteLine($"File I/O error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}