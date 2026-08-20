using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Entry point
    static async Task Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF with annotations
        const string apiEndpoint   = "https://example.com/api/upload"; // REST API URL

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export annotations to an in‑memory XFDF stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset for reading

                // Prepare HTTP client and multipart content
                using (HttpClient httpClient = new HttpClient())
                using (MultipartFormDataContent multipart = new MultipartFormDataContent())
                using (StreamContent fileContent = new StreamContent(xfdfStream))
                {
                    // Set the correct MIME type for XFDF
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");
                    // Add the file content; the third parameter is the filename sent to the server
                    multipart.Add(fileContent, "file", "annotations.xfdf");

                    // POST the XFDF to the REST endpoint
                    HttpResponseMessage response = await httpClient.PostAsync(apiEndpoint, multipart);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("XFDF uploaded successfully.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Upload failed: {(int)response.StatusCode} {response.ReasonPhrase}");
                    }
                }
            }
        }
    }
}