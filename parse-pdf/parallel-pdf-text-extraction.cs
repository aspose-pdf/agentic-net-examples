using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTextParallelExtractor
{
    /// <summary>
    /// Extracts text from multiple PDF files in parallel.
    /// </summary>
    /// <param name="pdfFilePaths">Array of PDF file paths to process.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>
    /// A task that resolves to a dictionary where the key is the PDF file path
    /// and the value is the extracted text for that document.
    /// </returns>
    public static async Task<Dictionary<string, string>> ExtractTextsAsync(
        string[] pdfFilePaths,
        CancellationToken cancellationToken = default)
    {
        if (pdfFilePaths == null) throw new ArgumentNullException(nameof(pdfFilePaths));

        var results = new Dictionary<string, string>();
        var lockObj = new object();
        var extractionTasks = new List<Task>();

        foreach (var path in pdfFilePaths)
        {
            string pdfPath = path;
            if (!File.Exists(pdfPath))
                continue;

            extractionTasks.Add(Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var doc = new Document(pdfPath))
                {
                    var absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string extractedText = absorber.Text ?? string.Empty;

                    lock (lockObj)
                    {
                        results[pdfPath] = extractedText;
                    }
                }
            }, cancellationToken));
        }

        await Task.WhenAll(extractionTasks).ConfigureAwait(false);
        return results;
    }
}

public static class Program
{
    public static async Task Main(string[] args)
    {
        // Determine the folder to scan – first argument or current directory.
        string folder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        string[] pdfFiles = Directory.GetFiles(folder, "*.pdf", SearchOption.TopDirectoryOnly);

        var texts = await PdfTextParallelExtractor.ExtractTextsAsync(pdfFiles);

        foreach (var kvp in texts)
        {
            Console.WriteLine($"File: {kvp.Key}");
            Console.WriteLine($"Extracted characters: {kvp.Value.Length}");
        }
    }
}