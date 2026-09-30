using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfSplitter
{
    /// <summary>
    /// Splits the input PDF stream into multiple bulk page sets.
    /// Each set contains up to <paramref name="bulkSize"/> pages.
    /// Returns an array of MemoryStream objects, each representing a subset PDF.
    /// </summary>
    /// <param name="pdfStream">Input PDF as a seekable stream.</param>
    /// <param name="bulkSize">Maximum number of pages per output set.</param>
    /// <returns>Array of MemoryStream, each containing a PDF fragment.</returns>
    public static MemoryStream[] SplitPdfIntoBulkSets(Stream pdfStream, int bulkSize)
    {
        if (pdfStream == null) throw new ArgumentNullException(nameof(pdfStream));
        if (!pdfStream.CanSeek) throw new ArgumentException("Stream must support seeking.", nameof(pdfStream));
        if (bulkSize <= 0) throw new ArgumentOutOfRangeException(nameof(bulkSize), "Bulk size must be greater than zero.");

        // Read the entire PDF into a byte array so it can be reused for each extraction.
        byte[] pdfBytes;
        using (MemoryStream temp = new MemoryStream())
        {
            pdfStream.CopyTo(temp);
            pdfBytes = temp.ToArray();
        }

        // Determine total page count using Document (wrapped in using for proper disposal).
        int pageCount;
        using (Document doc = new Document(new MemoryStream(pdfBytes)))
        {
            pageCount = doc.Pages.Count;
        }

        // Calculate how many bulk sets are needed.
        int setCount = (pageCount + bulkSize - 1) / bulkSize;
        var result = new MemoryStream[setCount];

        for (int i = 0; i < setCount; i++)
        {
            // Aspose.Pdf uses 1‑based page indexing.
            int startPage = i * bulkSize + 1;
            int endPage   = Math.Min(startPage + bulkSize - 1, pageCount);

            // Create a fresh source stream for each extraction.
            using (MemoryStream srcStream = new MemoryStream(pdfBytes))
            using (Document srcDoc = new Document(srcStream))
            {
                // Create a new document that will hold the selected page range.
                Document subsetDoc = new Document();

                for (int p = startPage; p <= endPage; p++)
                {
                    // Add a copy of each required page to the subset document.
                    subsetDoc.Pages.Add(srcDoc.Pages[p]);
                }

                MemoryStream destStream = new MemoryStream();
                subsetDoc.Save(destStream);
                destStream.Position = 0; // Reset for consumer reading.
                result[i] = destStream; // Store the resulting MemoryStream.
            }
        }

        return result;
    }

    // Minimal entry point to satisfy the compiler when the project is built as an executable.
    public static void Main(string[] args)
    {
        // The method is intentionally left empty – the library functionality is exercised
        // via the SplitPdfIntoBulkSets method from user code or unit tests.
    }
}
