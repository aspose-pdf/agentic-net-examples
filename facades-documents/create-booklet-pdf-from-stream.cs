using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class BookletCreator
{
    /// <summary>
    /// Creates a booklet PDF from the provided PDF input stream.
    /// The result is returned as a MemoryStream positioned at the beginning.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF.</param>
    /// <returns>MemoryStream with the booklet PDF.</returns>
    public static MemoryStream CreateBookletPdf(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is at the start.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream that will hold the booklet PDF.
        MemoryStream outputStream = new MemoryStream();

        // PdfFileEditor provides the MakeBooklet functionality.
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Create booklet using the stream overload.
        // This writes the result into outputStream.
        pdfEditor.MakeBooklet(inputPdfStream, outputStream);

        // Reset the output stream position so callers can read from the beginning.
        if (outputStream.CanSeek)
            outputStream.Position = 0;

        return outputStream;
    }
}

// Dummy entry point to satisfy the console‑application requirement.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is accessed via BookletCreator.
    }
}