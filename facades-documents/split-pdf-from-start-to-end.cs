using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfSplitter
{
    /// <summary>
    /// Splits the input PDF stream from the specified start page to the last page.
    /// The result is returned as a MemoryStream.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF. Must be readable and seekable.</param>
    /// <param name="startPage">1‑based index of the first page to include in the split.</param>
    /// <returns>MemoryStream with the extracted pages.</returns>
    public static MemoryStream SplitFromStartToEnd(Stream inputPdfStream, int startPage)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Start page must be >= 1.");

        // Ensure the stream is positioned at the beginning before processing.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Prepare the output stream.
        var output = new MemoryStream();
        var editor = new PdfFileEditor();

        // Split from the start page to the end of the document.
        bool success = editor.SplitToEnd(inputPdfStream, startPage, output);
        if (!success)
            throw new InvalidOperationException("Split operation failed.");

        // Reset position for the caller.
        output.Position = 0;
        return output;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfSplitter.
    }
}