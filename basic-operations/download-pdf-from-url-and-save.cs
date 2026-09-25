using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        // URL of the PDF to download
        const string pdfUrl = "https://example.com/sample.pdf";
        // Local path where the PDF will be saved
        const string localPath = "downloaded.pdf";

        try
        {
            // Use HttpClient (the modern, non‑obsolete API) to obtain the remote PDF stream
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(pdfUrl);
            response.EnsureSuccessStatusCode();

            await using var pdfStream = await response.Content.ReadAsStreamAsync();

            // Load the PDF from the remote stream into an Aspose.Pdf Document
            var doc = new Document(pdfStream);

            // Save the document to a local file
            doc.Save(localPath);

            Console.WriteLine($"PDF downloaded and saved to '{localPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error downloading PDF: {ex.Message}");
        }
    }
}
