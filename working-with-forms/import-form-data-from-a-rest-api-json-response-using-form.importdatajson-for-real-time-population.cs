using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string outputPdfPath   = "filled.pdf";    // Resulting PDF
        const string apiEndpoint     = "https://example.com/api/formdata"; // REST API returning JSON

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template not found: {pdfTemplatePath}");
            return;
        }

        try
        {
            // Load the PDF document containing the form
            using (Document doc = new Document(pdfTemplatePath))
            {
                // Retrieve JSON data from the REST API
                using (HttpClient httpClient = new HttpClient())
                using (HttpResponseMessage response = await httpClient.GetAsync(apiEndpoint))
                {
                    response.EnsureSuccessStatusCode();

                    // Stream the JSON directly into the ImportFromJson method
                    using (Stream jsonStream = await response.Content.ReadAsStreamAsync())
                    {
                        // Populate the form fields from the JSON stream
                        doc.Form.ImportFromJson(jsonStream);
                    }
                }

                // Save the populated PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"Form populated and saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}