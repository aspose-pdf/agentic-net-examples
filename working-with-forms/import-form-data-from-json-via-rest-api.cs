using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";
        const string apiEndpoint = "https://example.com/api/formdata";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        try
        {
            // Load the PDF document containing the form
            using (Document document = new Document(pdfTemplatePath))
            {
                // Retrieve JSON payload from the REST API
                using (HttpClient httpClient = new HttpClient())
                using (Stream jsonStream = await httpClient.GetStreamAsync(apiEndpoint))
                {
                    // Populate form fields from the JSON stream
                    document.Form.ImportFromJson(jsonStream);
                }

                // Save the updated PDF with populated form data
                document.Save(outputPdfPath);
            }

            Console.WriteLine($"Form successfully populated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}