using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfUtilities
{
    /// <summary>
    /// Flattens all annotations in a PDF provided as a stream and returns the modified PDF as a new stream.
    /// No file system access is performed.
    /// </summary>
    /// <param name="pdfInput">Stream containing the source PDF. The stream will be read from its current position.</param>
    /// <returns>A MemoryStream containing the PDF with flattened annotations. Caller is responsible for disposing it.</returns>
    public static MemoryStream FlattenAnnotations(Stream pdfInput)
    {
        // Ensure the input stream is positioned at the beginning
        if (pdfInput.CanSeek)
        {
            pdfInput.Position = 0;
        }

        // Output stream that will hold the flattened PDF
        MemoryStream outputStream = new MemoryStream();

        // Use PdfAnnotationEditor (Facades API) to bind, flatten, and save the document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF from the input stream
            editor.BindPdf(pdfInput);

            // Flatten all annotations in the document
            editor.FlatteningAnnotations();

            // Save the modified PDF into the output stream
            editor.Save(outputStream);
        }

        // Reset the output stream position so it can be read from the start
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
    public static void Main()
    {
        // Intentionally left blank – the library functionality is accessed via PdfUtilities.
    }
}