using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdfPath = "input_form.pdf";
        const string outputPdfPath = "filled_form.pdf";

        // URL of the REST API that returns JSON data for the form fields
        const string jsonApiUrl = "https://example.com/api/formdata";

        // Download the JSON payload from the REST API
        Stream jsonStream = null;
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(jsonApiUrl).Result;
                response.EnsureSuccessStatusCode();
                jsonStream = response.Content.ReadAsStreamAsync().Result;
            }

            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Import form field values from the JSON stream
                // Core API method: ImportFromJson(Stream)
                doc.Form.ImportFromJson(jsonStream);

                // Save the updated PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Ensure the JSON stream is properly disposed
            jsonStream?.Dispose();
        }
    }
}