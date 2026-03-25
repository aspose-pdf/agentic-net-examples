using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        const string pdfUrl = "https://example.com/sample.pdf"; // replace with a valid URL
        const string localPath = "sample.pdf";

        try
        {
            using var httpClient = new HttpClient();

            // Request the PDF and make sure the response is successful (status 2xx)
            using var response = await httpClient.GetAsync(pdfUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            // Obtain the response stream without loading the whole file into memory
            await using var networkStream = await response.Content.ReadAsStreamAsync();

            // Load the PDF from the network stream
            var pdfDocument = new Document(networkStream);

            // Save the PDF to a local file
            pdfDocument.Save(localPath);

            Console.WriteLine($"PDF downloaded and saved to '{localPath}'.");
        }
        catch (HttpRequestException ex)
        {
            // Handles 4xx/5xx HTTP errors (e.g., 404 Not Found)
            Console.Error.WriteLine($"Failed to download PDF: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
