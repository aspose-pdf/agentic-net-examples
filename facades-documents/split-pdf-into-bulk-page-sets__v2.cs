using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

public static class PdfSplitter
{
    /// <summary>
    /// Splits the PDF contained in <paramref name="inputPdfStream"/> into multiple
    /// bulk page sets, each set containing up to <paramref name="pagesPerSet"/> pages.
    /// Returns an array of MemoryStream objects, each representing a split PDF.
    /// </summary>
    public static MemoryStream[] SplitPdfIntoBulkSets(Stream inputPdfStream, int pagesPerSet)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (pagesPerSet <= 0) throw new ArgumentOutOfRangeException(nameof(pagesPerSet));

        // Ensure the stream is positioned at the beginning before loading.
        inputPdfStream.Position = 0;

        // Load the source PDF using the Document class (no BindPdf needed).
        Document sourceDoc = new Document(inputPdfStream);
        int totalPages = sourceDoc.Pages.Count;

        var resultStreams = new List<MemoryStream>();
        int setCount = (totalPages + pagesPerSet - 1) / pagesPerSet;

        for (int i = 0; i < setCount; i++)
        {
            int startPage = i * pagesPerSet + 1; // 1‑based indexing
            int endPage = Math.Min(startPage + pagesPerSet - 1, totalPages);

            // Create a new document for the current bulk set.
            Document splitDoc = new Document();

            // Copy the required pages from the source document.
            for (int p = startPage; p <= endPage; p++)
            {
                // The Add method clones the page into the new document.
                splitDoc.Pages.Add(sourceDoc.Pages[p]);
            }

            // Save the split document into a memory stream.
            MemoryStream outStream = new MemoryStream();
            splitDoc.Save(outStream, SaveFormat.Pdf);
            outStream.Position = 0; // Reset for reading by the caller.
            resultStreams.Add(outStream);
        }

        return resultStreams.ToArray();
    }
}

// Minimal entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The library method can be called from here if needed.
        // Example (commented out to avoid runtime errors when no PDF is supplied):
        // using var fs = File.OpenRead("sample.pdf");
        // var parts = PdfSplitter.SplitPdfIntoBulkSets(fs, 10);
        // Console.WriteLine($"Created {parts.Length} split PDFs.");
    }
}
