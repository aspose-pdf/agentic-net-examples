using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class PdfMergeUtility
{
    // Merges PDF files obtained from the specified URLs and saves the result to outputPath.
    // No intermediate files are created; PDFs are streamed directly.
    public static void MergeFromUrls(string[] pdfUrls, string outputPath)
    {
        // List to hold the open network streams for each PDF.
        List<Stream> inputStreams = new List<Stream>();

        // Single HttpClient instance for all requests (recommended practice).
        using (HttpClient httpClient = new HttpClient())
        {
            foreach (string url in pdfUrls)
            {
                // Synchronously download the PDF content as a stream.
                // The stream remains open until the merge operation completes.
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                Stream pdfStream = response.Content.ReadAsStreamAsync().Result;
                inputStreams.Add(pdfStream);
            }
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Create the output file stream where the merged PDF will be written.
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Concatenate all input streams into the output stream.
            // This method merges PDFs without creating temporary files.
            editor.Concatenate(inputStreams.ToArray(), outputStream);
        }

        // Dispose all input streams now that merging is finished.
        foreach (Stream s in inputStreams)
        {
            s.Dispose();
        }
    }

    // Example usage.
    static void Main()
    {
        string[] pdfUrls = new string[]
        {
            "https://example.com/doc1.pdf",
            "https://example.com/doc2.pdf",
            "https://example.com/doc3.pdf"
        };

        string outputPath = "merged_result.pdf";

        try
        {
            MergeFromUrls(pdfUrls, outputPath);
            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}