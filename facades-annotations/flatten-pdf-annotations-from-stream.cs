using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnnotationFlattener
{
    /// <summary>
    /// Flattens all annotations in a PDF provided as a stream and returns a new stream containing the modified PDF.
    /// No file system access is performed.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF. The stream must be readable and seekable.</param>
    /// <returns>A MemoryStream with the flattened PDF. Caller is responsible for disposing the returned stream.</returns>
    public static MemoryStream FlattenAnnotations(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream will hold the result.
        MemoryStream outputPdfStream = new MemoryStream();

        // PdfAnnotationEditor is a facade that works directly with streams.
        // It implements IDisposable, so we use a using block for deterministic cleanup.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF from the input stream.
            editor.BindPdf(inputPdfStream);

            // Flatten all annotations in the document.
            editor.FlatteningAnnotations();

            // Save the modified PDF into the output stream.
            editor.Save(outputPdfStream);
        }

        // Reset the position of the output stream so callers can read from the start.
        if (outputPdfStream.CanSeek)
            outputPdfStream.Position = 0;

        return outputPdfStream;
    }
}

// Dummy entry point required for console‑type projects.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library method can be called from other code.
    }
}
