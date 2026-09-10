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
        var outputStream = new MemoryStream();

        // PdfFileEditor provides the MakeBooklet operation.
        var editor = new PdfFileEditor();

        // Perform the booklet conversion.
        editor.MakeBooklet(inputPdfStream, outputStream);

        // Reset the position to the beginning so the caller can read from the start.
        outputStream.Position = 0;

        return outputStream;
    }
}

// Dummy entry point required when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is accessed via BookletCreator.
    }
}