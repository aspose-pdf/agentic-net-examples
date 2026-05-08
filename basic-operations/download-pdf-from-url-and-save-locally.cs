using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main(string[] args)
    {
        // URL of the PDF to download – using a publicly reachable sample PDF.
        const string pdfUrl = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf";
        // Local path where the PDF will be saved
        const string outputPath = "sample_downloaded.pdf";

        // Use HttpClient (the modern replacement for WebRequest) to download the PDF as a stream
        using var httpClient = new HttpClient();
        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync(pdfUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send request: {ex.Message}");
            return;
        }

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Unable to download PDF. HTTP {(int)response.StatusCode} – {response.ReasonPhrase}");
            return;
        }

        // Get the response content as a stream
        await using var pdfStream = await response.Content.ReadAsStreamAsync();

        // Load the PDF from the stream into an Aspose.Pdf Document
        using var pdfDocument = new Document(pdfStream);

        // Save the document locally as a PDF file
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}
