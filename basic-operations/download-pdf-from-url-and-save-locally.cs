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
            // Use HttpClient (the modern, non‑obsolete API) to obtain the PDF stream
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(pdfUrl);
            response.EnsureSuccessStatusCode(); // throws if status is not 2xx

            await using Stream pdfStream = await response.Content.ReadAsStreamAsync();

            // Load the PDF from the stream into an Aspose.Pdf Document
            using Document pdfDocument = new Document(pdfStream);
            // Save the document to a local file
            pdfDocument.Save(localPath);

            Console.WriteLine($"PDF downloaded from '{pdfUrl}' and saved to '{localPath}'.");
        }
        catch (HttpRequestException ex)
        {
            // Handle network‑level errors (e.g., 404, timeout, DNS failure)
            Console.WriteLine($"Error downloading PDF: {ex.Message}");
        }
    }
}
