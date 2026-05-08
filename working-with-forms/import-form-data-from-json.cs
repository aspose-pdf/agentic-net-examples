using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Forms;        // For form handling (ImportFromJson)

class Program
{
    // Entry point – async to allow awaiting the HTTP request
    static async Task Main()
    {
        const string templatePath = "TemplateForm.pdf";   // PDF with form fields
        const string outputPath   = "PopulatedForm.pdf"; // Resulting PDF
        const string apiUrl       = "https://example.com/api/formdata"; // REST endpoint returning JSON

        // Verify that the template PDF exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Load the PDF document containing the form
        using (Document pdfDoc = new Document(templatePath))
        {
            // Create an HttpClient to fetch the JSON payload
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Request the JSON data as a stream (no need to load the whole string into memory)
                    using (Stream jsonStream = await httpClient.GetStreamAsync(apiUrl))
                    {
                        // Import the JSON data into the PDF form fields.
                        // Document.Form.ImportFromJson matches fields by their full names.
                        pdfDoc.Form.ImportFromJson(jsonStream);
                    }

                    // Save the updated PDF with populated form fields
                    pdfDoc.Save(outputPath);
                    Console.WriteLine($"Form data imported and PDF saved to '{outputPath}'.");
                }
                catch (HttpRequestException httpEx)
                {
                    Console.Error.WriteLine($"Error fetching JSON from API: {httpEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}