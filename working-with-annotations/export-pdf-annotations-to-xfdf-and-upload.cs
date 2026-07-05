using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        const string inputPdf = "input.pdf";
        const string apiUrl   = "https://example.com/api/upload";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Export annotations to an in‑memory XFDF stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // rewind for reading

                // Upload the XFDF to the REST endpoint
                using (HttpClient client = new HttpClient())
                using (MultipartFormDataContent multipart = new MultipartFormDataContent())
                {
                    StreamContent fileContent = new StreamContent(xfdfStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");
                    multipart.Add(fileContent, "file", "annotations.xfdf");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, multipart);
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