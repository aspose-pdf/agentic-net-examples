using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfMergeUtility
{
    // Merges PDF files obtained from network URLs and saves the combined PDF to a local file.
    // No intermediate files are created; PDFs are kept in memory streams.
    public static async Task MergeFromUrlsAsync(IEnumerable<string> pdfUrls, string outputFilePath)
    {
        // HttpClient is used to fetch PDF data from the provided URLs.
        using (HttpClient httpClient = new HttpClient())
        {
            // Holds the in‑memory streams for each downloaded PDF.
            List<Stream> sourceStreams = new List<Stream>();
            try
            {
                // Download each PDF and copy it into a MemoryStream.
                foreach (string url in pdfUrls)
                {
                    // Get the response and ensure the request succeeded.
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // throws if status is not 2xx

                    // Read the content into a byte array and wrap it with a MemoryStream.
                    byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();
                    MemoryStream memory = new MemoryStream(pdfBytes);
                    memory.Position = 0;               // Reset for reading by Aspose.
                    sourceStreams.Add(memory);          // Keep the stream for later concatenation.
                }

                // Create the output file stream where the merged PDF will be written.
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    // PdfFileEditor concatenates an array of input streams into the output stream.
                    PdfFileEditor editor = new PdfFileEditor();
                    editor.Concatenate(sourceStreams.ToArray(), outputStream);
                }
            }
            finally
            {
                // Ensure all memory streams are disposed to free resources.
                foreach (Stream s in sourceStreams)
                {
                    s.Dispose();
                }
            }
        }
    }

    // Example entry point.
    static async Task Main()
    {
        // List of PDF URLs to merge.
        var pdfUrls = new[]
        {
            "https://example.com/doc1.pdf",
            "https://example.com/doc2.pdf",
            "https://example.com/doc3.pdf"
        };

        // Destination path for the merged PDF.
        string outputPath = "merged_output.pdf";

        try
        {
            // Perform the merge.
            await MergeFromUrlsAsync(pdfUrls, outputPath);
            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Failed to download one or more PDFs: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
