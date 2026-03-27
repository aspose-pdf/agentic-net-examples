using System;
using System.IO;
using Aspose.Pdf.Facades;

public class BookletCreator
{
    public static MemoryStream CreateBookletPdf(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        var outputPdfStream = new MemoryStream();
        var pdfEditor = new PdfFileEditor();
        bool success = pdfEditor.MakeBooklet(inputPdfStream, outputPdfStream);
        if (!success)
            throw new InvalidOperationException("Failed to create booklet PDF.");

        outputPdfStream.Position = 0;
        return outputPdfStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // Placeholder – no operation required for the library functionality.
    }
}