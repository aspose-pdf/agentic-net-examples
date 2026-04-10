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

        // HttpClient is the modern replacement for WebRequest/HttpWebRequest
        using var httpClient = new HttpClient();

        try
        {
            // Get the response stream directly – HttpClient handles redirects, time‑outs, etc.
            await using Stream responseStream = await httpClient.GetStreamAsync(pdfUrl);

            // Load the PDF from the response stream into an Aspose.Pdf Document
            using var pdfDoc = new Document(responseStream);

            // Save the document to a local file
            pdfDoc.Save(localPath);

            Console.WriteLine($"PDF downloaded from '{pdfUrl}' and saved to '{localPath}'.");
        }
        catch (HttpRequestException httpEx)
        {
            // Handles 404, network errors, time‑outs, etc.
            Console.WriteLine($"Failed to download PDF: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            // Any other unexpected errors
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}