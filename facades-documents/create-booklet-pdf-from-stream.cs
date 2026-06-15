using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class BookletCreator
{
    /// <summary>
    /// Creates a booklet PDF from the provided input PDF stream.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF.</param>
    /// <returns>MemoryStream with the booklet PDF.</returns>
    public static MemoryStream CreateBooklet(Stream inputPdfStream)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream that will hold the booklet PDF.
        MemoryStream outputStream = new MemoryStream();

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the booklet conversion. Use the overload that accepts streams.
        editor.MakeBooklet(inputPdfStream, outputStream);

        // Reset the output stream position for reading by the caller.
        if (outputStream.CanSeek)
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
    }
}