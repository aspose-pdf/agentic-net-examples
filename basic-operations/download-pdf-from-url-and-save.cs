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
        // Local file path where the PDF will be saved
        const string localPath = "sample_downloaded.pdf";

        // Use HttpClient (the modern replacement for WebRequest) to download the PDF
        using var httpClient = new HttpClient();
        try
        {
            // Get the response stream
            using Stream networkStream = await httpClient.GetStreamAsync(pdfUrl);

            // Copy the network stream into a seek‑able MemoryStream because Aspose.Pdf.Document
            // expects a stream that supports seeking.
            using var memoryStream = new MemoryStream();
            await networkStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // Reset position to the beginning

            // Load the PDF from the memory stream using Aspose.Pdf Document constructor
            using var pdfDocument = new Document(memoryStream);

            // Save the PDF to the local file system
            pdfDocument.Save(localPath);

            Console.WriteLine($"PDF successfully saved to '{localPath}'.");
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Error downloading PDF: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
