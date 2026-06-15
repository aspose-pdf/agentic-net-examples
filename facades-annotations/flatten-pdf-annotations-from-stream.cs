using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Flattens all annotations in a PDF provided as a stream and returns a new stream containing the modified PDF.
    /// </summary>
    /// <param name="pdfInput">Input PDF stream (must be readable and seekable).</param>
    /// <returns>A MemoryStream with the flattened PDF. Caller is responsible for disposing the returned stream.</returns>
    public static Stream FlattenAnnotations(Stream pdfInput)
    {
        if (pdfInput == null) throw new ArgumentNullException(nameof(pdfInput));
        // Ensure the input stream is positioned at the beginning.
        if (pdfInput.CanSeek) pdfInput.Position = 0;

        // Output stream will hold the result.
        MemoryStream outputStream = new MemoryStream();

        // Use PdfAnnotationEditor from Aspose.Pdf.Facades to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the input PDF stream to the editor.
            editor.BindPdf(pdfInput);

            // Flatten all annotations in the document.
            editor.FlatteningAnnotations();

            // Save the modified document to the output stream.
            editor.Save(outputStream);
        }

        // Reset the output stream position for the caller to read from the beginning.
        if (outputStream.CanSeek) outputStream.Position = 0;
        return outputStream;
    }
}

// Dummy entry point required for compilation as a console application.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank. The library functionality is exposed via PdfAnnotationHelper.
    }
}