using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Entry point supports async for proper async I/O.
    static async Task Main(string[] args)
    {
        // URLs can be supplied via command‑line arguments or hard‑coded for testing.
        // Example usage: dotnet run https://example.com/file1.pdf https://example.com/file2.pdf
        if (args.Length < 2)
        {
            Console.WriteLine("Please provide two PDF URLs as command‑line arguments.");
            return;
        }

        string firstPdfUrl = args[0];
        string secondPdfUrl = args[1];
        const string outputFile = "merged.pdf";

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Download the PDFs directly into memory – no temporary files are created.
                byte[] firstBytes = await GetPdfBytesAsync(httpClient, firstPdfUrl);
                byte[] secondBytes = await GetPdfBytesAsync(httpClient, secondPdfUrl);

                using (MemoryStream firstStream = new MemoryStream(firstBytes))
                using (MemoryStream secondStream = new MemoryStream(secondBytes))
                {
                    // Load the PDFs with Aspose.Pdf.Document.
                    Document firstDoc = new Document(firstStream);
                    Document secondDoc = new Document(secondStream);

                    // Append all pages of the second document to the first one.
                    firstDoc.Pages.Add(secondDoc.Pages);

                    // Save the merged document to disk.
                    firstDoc.Save(outputFile);
                }

                Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
            }
            catch (InvalidOperationException ex)
            {
                // The custom exception from GetPdfBytesAsync already contains a clear message.
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Unexpected errors – log and exit.
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Retrieves the PDF content as a byte array. Throws an InvalidOperationException with a clear
    /// message if the request fails.
    /// </summary>
    private static async Task<byte[]> GetPdfBytesAsync(HttpClient client, string url)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to download PDF from '{url}'.", ex);
        }
    }
}
