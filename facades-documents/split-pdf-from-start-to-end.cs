using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfSplitter
{
    /// <summary>
    /// Splits the input PDF stream from the specified start page to the end of the document.
    /// The resulting PDF is returned as a MemoryStream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF. Must be readable.</param>
    /// <param name="startPage">1‑based page number where the split should begin.</param>
    /// <returns>MemoryStream holding the split PDF (pages startPage … last page).</returns>
    public static MemoryStream SplitFromStartToEnd(Stream inputPdf, int startPage)
    {
        if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
        if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Page numbers are 1‑based.");

        // Output stream that will receive the rear part of the document.
        MemoryStream outputStream = new MemoryStream();

        // PdfFileEditor does NOT implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // SplitToEnd extracts pages from 'startPage' to the last page.
        // The method does NOT close the streams, so we keep them open for the caller.
        editor.SplitToEnd(inputPdf, startPage, outputStream);

        // Reset the position so the caller can read from the beginning.
        outputStream.Position = 0;

        return outputStream;
    }

    // Entry point required for compilation when the project is built as an executable.
    // It simply demonstrates a basic call; in real usage the method can be invoked from elsewhere.
    public static void Main(string[] args)
    {
        // Example usage – this block is optional and can be removed in production.
        // It is kept minimal to avoid dependencies on external files during a simple build.
        // If you have an input PDF, uncomment and adjust the paths accordingly.
        //
        // using (FileStream source = new FileStream("input.pdf", FileMode.Open, FileAccess.Read))
        // {
        //     MemoryStream result = SplitFromStartToEnd(source, 1);
        //     using (FileStream dest = new FileStream("output.pdf", FileMode.Create, FileAccess.Write))
        //     {
        //         result.CopyTo(dest);
        //     }
        // }
        
        // No operation – the presence of Main satisfies the compiler.
    }
}
