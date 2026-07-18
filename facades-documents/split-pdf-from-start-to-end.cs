using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfSplitter
{
    /// <summary>
    /// Splits the input PDF stream from the specified start page to the end
    /// and returns the resulting PDF as a MemoryStream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF. Must be readable.</param>
    /// <param name="startPage">1‑based page number where the split should begin.</param>
    /// <returns>MemoryStream with the split PDF (from startPage to the last page).</returns>
    public static MemoryStream SplitFromStartToEnd(Stream inputPdf, int startPage)
    {
        if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
        if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Page numbers are 1‑based.");

        // Output stream that will hold the split PDF.
        var outputPdf = new MemoryStream();

        // PdfFileEditor provides the SplitToEnd method which extracts the
        // rear part of the document starting at the given page.
        var editor = new PdfFileEditor();

        // Perform the split. The method returns true on success.
        bool success = editor.SplitToEnd(inputPdf, startPage, outputPdf);
        if (!success)
        {
            throw new InvalidOperationException("Failed to split the PDF document.");
        }

        // Reset the position of the output stream so it can be read from the beginning.
        outputPdf.Position = 0;
        return outputPdf;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // Example usage (can be removed or replaced in production code):
        // using var input = File.OpenRead("sample.pdf");
        // var result = PdfSplitter.SplitFromStartToEnd(input, 3);
        // File.WriteAllBytes("output.pdf", result.ToArray());
    }
}