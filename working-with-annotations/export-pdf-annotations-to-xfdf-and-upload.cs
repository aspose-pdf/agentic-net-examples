using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aspose.Pdf; // Aspose.Pdf namespace provides Document and related classes

class Program
{
    // Entry point – async to allow awaiting the HTTP request
    static async Task Main()
    {
        // Path to the source PDF that contains annotations
        const string pdfPath = "input.pdf";

        // REST API endpoint that accepts the XFDF file
        const string apiUrl = "https://example.com/api/upload-xfdf";

        // Validate input file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create an HttpClient instance (should be reused in real applications)
        using (HttpClient httpClient = new HttpClient())
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Export annotations to an in‑memory XFDF stream (uses ExportAnnotationsToXfdf(Stream))
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
                    xfdfStream.Position = 0; // Reset stream for reading

                    // Prepare HTTP content for the XFDF file
                    StreamContent xfdfContent = new StreamContent(xfdfStream);
                    xfdfContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");

                    // Use multipart/form-data to send the file (field name "file")
                    using (MultipartFormDataContent multipart = new MultipartFormDataContent())
                    {
                        multipart.Add(xfdfContent, "file", "annotations.xfdf");

                        // Send POST request to the REST API
                        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, multipart);

                        // Check response status
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("XFDF file uploaded successfully.");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Upload failed. Status: {response.StatusCode}");
                            string errorBody = await response.Content.ReadAsStringAsync();
                            Console.Error.WriteLine($"Server response: {errorBody}");
                        }
                    }
                }
            }
        }
    }
}