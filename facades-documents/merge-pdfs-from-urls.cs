using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf.Facades;

class PdfMergeUtility
{
    /// <summary>
    /// Merges PDF files obtained from the specified URLs and saves the combined PDF to a local file.
    /// The PDFs are streamed directly without creating intermediate temporary files.
    /// </summary>
    /// <param name="pdfUrls">Array of HTTP/HTTPS URLs pointing to PDF files.</param>
    /// <param name="outputFilePath">Full path of the resulting merged PDF file.</param>
    public static void MergePdfsFromUrls(string[] pdfUrls, string outputFilePath)
    {
        if (pdfUrls == null || pdfUrls.Length == 0)
            throw new ArgumentException("At least one PDF URL must be provided.", nameof(pdfUrls));

        // Prepare an HttpClient for downloading the PDF streams.
        using (HttpClient httpClient = new HttpClient())
        {
            // Create an array to hold the input streams.
            Stream[] inputStreams = new Stream[pdfUrls.Length];

            try
            {
                // Download each PDF as a stream.
                for (int i = 0; i < pdfUrls.Length; i++)
                {
                    // GetStreamAsync returns a Task<Stream>; we block here for simplicity.
                    // In production code you would make this method async.
                    inputStreams[i] = httpClient.GetStreamAsync(pdfUrls[i]).Result;
                }

                // Open the output file stream where the merged PDF will be written.
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    // PdfFileEditor performs concatenation directly on streams.
                    PdfFileEditor editor = new PdfFileEditor
                    {
                        // Ensure that the input streams are closed after concatenation.
                        CloseConcatenatedStreams = true
                    };

                    // Concatenate all input streams into the output stream.
                    // This overload accepts an array of input streams and a single output stream.
                    bool success = editor.Concatenate(inputStreams, outputStream);

                    if (!success)
                    {
                        throw new InvalidOperationException("PDF concatenation failed. Check the input streams for validity.");
                    }
                }
            }
            finally
            {
                // Ensure all input streams are disposed in case CloseConcatenatedStreams was false or an exception occurred.
                foreach (var stream in inputStreams)
                {
                    stream?.Dispose();
                }
            }
        }
    }

    // Example usage.
    static void Main()
    {
        // URLs of PDFs to merge (replace with actual accessible URLs).
        string[] pdfUrls = new[]
        {
            "https://example.com/documents/file1.pdf",
            "https://example.com/documents/file2.pdf",
            "https://example.com/documents/file3.pdf"
        };

        // Destination path for the merged PDF.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "merged_output.pdf");

        try
        {
            MergePdfsFromUrls(pdfUrls, outputPath);
            Console.WriteLine($"Merged PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF merge: {ex.Message}");
        }
    }
}