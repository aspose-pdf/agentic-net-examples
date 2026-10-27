using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfSplitter
{
    /// <summary>
    /// Splits the input PDF stream from the specified start page to the last page
    /// and returns the resulting PDF as a MemoryStream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF (must be readable and seekable).</param>
    /// <param name="startPage">1‑based page number to start the split (inclusive).</param>
    /// <returns>MemoryStream with the extracted pages.</returns>
    public static MemoryStream SplitFromStartToEnd(Stream inputPdf, int startPage)
    {
        if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
        if (!inputPdf.CanSeek) throw new ArgumentException("Input stream must support seeking.", nameof(inputPdf));
        if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Start page must be >= 1.");

        // Output stream that will hold the split PDF
        var output = new MemoryStream();
        var editor = new PdfFileEditor();

        // SplitToEnd returns true on success; it writes the result into the output stream
        bool success = editor.SplitToEnd(inputPdf, startPage, output);
        if (!success)
            throw new InvalidOperationException("Split operation failed.");

        // Rewind the stream so the caller can read from the beginning
        output.Position = 0;
        return output;
    }
}

// Minimal entry point to satisfy the console‑app project configuration.
public class Program
{
    public static void Main(string[] args)
    {
        // No implementation required for the library functionality.
        // This stub exists solely to provide a valid Main method.
    }
}