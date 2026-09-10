using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfUtilities
{
    /// <summary>
    /// Flattens all annotations in a PDF provided as a stream and returns a new stream containing the modified PDF.
    /// The operation is performed entirely in memory; no files are written to disk.
    /// </summary>
    /// <param name="pdfInput">Stream containing the source PDF. The stream must be readable and seekable.</param>
    /// <returns>A MemoryStream with the flattened PDF. Caller is responsible for disposing the returned stream.</returns>
    public static Stream FlattenPdfAnnotations(Stream pdfInput)
    {
        // Ensure the input stream is positioned at the beginning.
        if (pdfInput.CanSeek)
        {
            pdfInput.Position = 0;
        }

        // Output stream that will hold the flattened PDF.
        MemoryStream outputStream = new MemoryStream();

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document from the input stream.
            editor.BindPdf(pdfInput);

            // Flatten all annotations in the document.
            editor.FlatteningAnnotations();

            // Save the modified document to the output stream.
            editor.Save(outputStream);
        }

        // Reset the position of the output stream so it can be read from the beginning.
        if (outputStream.CanSeek)
        {
            outputStream.Position = 0;
        }

        return outputStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is accessed via PdfUtilities.
    }
}