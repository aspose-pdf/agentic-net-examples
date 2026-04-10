using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfSplitter
{
    /// <summary>
    /// Splits a PDF supplied as a stream into multiple bulk documents.
    /// Each bulk is defined by a start and end page (1‑based, inclusive).
    /// Returns an array of MemoryStream objects, each containing a PDF document.
    /// </summary>
    /// <param name="inputPdfStream">Stream that holds the source PDF. Must remain open after the call.</param>
    /// <param name="pageRanges">
    /// Array of int[2] where each inner array contains { startPage, endPage }.
    /// Example: new int[][] { new[] {1, 3}, new[] {4, 6} } creates two bulks (pages 1‑3 and 4‑6).
    /// </param>
    /// <returns>Array of MemoryStream, each positioned at the beginning of its PDF data.</returns>
    public static MemoryStream[] SplitPdfToBulks(Stream inputPdfStream, int[][] pageRanges)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (pageRanges == null) throw new ArgumentNullException(nameof(pageRanges));

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // SplitToBulks returns MemoryStream[]; the streams are NOT closed by the method.
        MemoryStream[] bulks = editor.SplitToBulks(inputPdfStream, pageRanges);

        // Ensure each MemoryStream is ready for reading by resetting its position.
        foreach (MemoryStream ms in bulks)
        {
            if (ms.CanSeek)
                ms.Position = 0;
        }

        return bulks;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Entry point required for compilation. No operational code needed for the library.
        // Example placeholder – can be removed or expanded as needed.
    }
}