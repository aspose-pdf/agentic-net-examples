using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        const string pdfUrl = "https://example.com/sample.pdf";
        const string localPath = "downloaded.pdf";

        try
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(pdfUrl);
            response.EnsureSuccessStatusCode();

            await using var pdfStream = await response.Content.ReadAsStreamAsync();
            var pdfDocument = new Document(pdfStream);
            pdfDocument.Save(localPath);

            Console.WriteLine($"PDF downloaded and saved to '{localPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}