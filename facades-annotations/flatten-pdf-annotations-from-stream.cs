using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Flattens all annotations in a PDF provided as a stream and returns a new stream
    /// containing the modified PDF. No temporary files are created.
    /// </summary>
    /// <param name="pdfInput">Input PDF stream (must be readable and seekable).</param>
    /// <returns>A MemoryStream with the flattened PDF.</returns>
    public static Stream FlattenAnnotations(Stream pdfInput)
    {
        if (pdfInput == null) throw new ArgumentNullException(nameof(pdfInput));

        // Ensure the input stream is positioned at the beginning.
        if (pdfInput.CanSeek)
            pdfInput.Position = 0;

        // Load the PDF document from the input stream.
        Document doc = new Document(pdfInput);

        // Create the annotation editor and bind the loaded document.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(doc);

        // Flatten all remaining annotations.
        editor.FlatteningAnnotations(); // <-- correct method name

        // Save the modified document into a memory stream.
        MemoryStream outputStream = new MemoryStream();
        doc.Save(outputStream);

        // Reset the output stream position so callers can read from the start.
        if (outputStream.CanSeek)
            outputStream.Position = 0;

        return outputStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is exposed via PdfAnnotationHelper.
    }
}