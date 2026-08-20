using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Aspose.Pdf.Facades;

class PdfMergeUtility
{
    // Merges multiple PDF streams into a single PDF file without creating intermediate files.
    public static void MergePdfStreams(IEnumerable<Stream> pdfStreams, string outputFilePath)
    {
        // Ensure each input stream is positioned at the beginning.
        var inputList = new List<Stream>();
        foreach (var s in pdfStreams)
        {
            if (s.CanSeek) s.Position = 0;
            inputList.Add(s);
        }

        // Write the concatenated PDF directly to the output file.
        using (FileStream outStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(inputList.ToArray(), outStream);
        }
    }

    // Example entry point demonstrating merging PDFs obtained from network streams.
    static void Main()
    {
        // URLs of PDF files to merge – replace with real URLs.
        string[] pdfUrls = { "https://example.com/doc1.pdf", "https://example.com/doc2.pdf" };

        var downloadedStreams = new List<Stream>();

        using (HttpClient httpClient = new HttpClient())
        {
            foreach (string url in pdfUrls)
            {
                // Download the PDF content.
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    // Log the failure and skip this URL instead of throwing an exception.
                    Console.WriteLine($"Unable to download '{url}'. HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
                    continue;
                }

                // Read the response stream into a MemoryStream that lives beyond the HttpResponseMessage.
                using (Stream responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    responseStream.CopyTo(memoryStream);
                    memoryStream.Position = 0; // Reset for reading.
                    downloadedStreams.Add(memoryStream);
                }
            }
        }

        if (downloadedStreams.Count == 0)
        {
            Console.WriteLine("No PDF files were downloaded successfully. Exiting.");
            return;
        }

        string outputPath = "merged.pdf";
        MergePdfStreams(downloadedStreams, outputPath);

        // Clean‑up temporary memory streams.
        foreach (var ms in downloadedStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
