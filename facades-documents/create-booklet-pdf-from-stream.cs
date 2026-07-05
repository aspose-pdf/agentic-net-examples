using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class BookletCreator
{
    /// <summary>
    /// Creates a booklet PDF from the provided input PDF stream.
    /// The result is returned as a MemoryStream.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF.</param>
    /// <returns>MemoryStream with the booklet PDF.</returns>
    public static MemoryStream CreateBookletPdf(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        // Output stream that will hold the booklet PDF.
        MemoryStream outputStream = new MemoryStream();

        // PdfFileEditor provides the MakeBooklet operation.
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the booklet conversion. Returns true on success.
        bool success = editor.MakeBooklet(inputPdfStream, outputStream);
        if (!success)
            throw new InvalidOperationException("Failed to create booklet PDF.");

        // Reset position so the caller can read from the beginning.
        outputStream.Position = 0;
        return outputStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via BookletCreator.
        // This method exists solely to provide a valid static Main entry point.
    }
}
