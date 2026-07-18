using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Entry point
    static async Task Main(string[] args)
    {
        // Validate arguments: input PDF path and REST endpoint URL
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf-path> <rest-endpoint-url>");
            return;
        }

        string pdfPath = args[0];
        string endpointUrl = args[1];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Export annotations to XFDF in memory and upload
        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export annotations to a MemoryStream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // ExportAnnotationsToXfdf writes XFDF data into the provided stream
                    doc.ExportAnnotationsToXfdf(xfdfStream);

                    // Reset stream position before reading
                    xfdfStream.Position = 0;

                    // Upload the XFDF content to the REST API
                    await UploadXfdfAsync(xfdfStream, endpointUrl);
                }
            }

            Console.WriteLine("Annotations exported and uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Sends the XFDF stream to the specified REST endpoint using HTTP POST
    private static async Task UploadXfdfAsync(Stream xfdfStream, string url)
    {
        // Prepare HttpClient (should be reused in real applications)
        using (HttpClient client = new HttpClient())
        {
            // Create content for the request
            StreamContent content = new StreamContent(xfdfStream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");

            // Optionally, use multipart/form-data if the API expects a file field
            MultipartFormDataContent multipart = new MultipartFormDataContent();
            multipart.Add(content, "file", "annotations.xfdf");

            // Perform POST request
            HttpResponseMessage response = await client.PostAsync(url, multipart);
            response.EnsureSuccessStatusCode(); // Throw if not successful
        }
    }
}