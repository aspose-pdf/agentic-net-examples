using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Flattens all annotations in a PDF provided as a stream and returns a new stream
    /// containing the modified PDF. No temporary files are written to disk.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF. Must be readable.</param>
    /// <returns>A MemoryStream with the flattened PDF. Caller is responsible for disposing it.</returns>
    public static Stream FlattenPdfAnnotations(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream that will hold the result.
        var outputStream = new MemoryStream();

        // Use PdfAnnotationEditor inside a using block to guarantee disposal.
        using (var annotationEditor = new PdfAnnotationEditor())
        {
            // Load the PDF from the input stream.
            annotationEditor.BindPdf(inputPdfStream);

            // Flatten all annotations (they become part of the page content).
            // NOTE: The correct method name is FlatteningAnnotations(), not FlattenAnnotations().
            annotationEditor.FlatteningAnnotations();

            // Save the modified PDF into the output stream.
            annotationEditor.Save(outputStream);
        }

        // Reset the output stream position so the caller can read from the start.
        if (outputStream.CanSeek)
            outputStream.Position = 0;

        return outputStream;
    }
}

// Dummy entry point to satisfy the compiler for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // The method is optional – it simply demonstrates usage when arguments are supplied.
        if (args.Length == 0)
            return;

        string inputPath = args[0];
        using var inputStream = File.OpenRead(inputPath);
        using var flattenedStream = PdfAnnotationHelper.FlattenPdfAnnotations(inputStream);

        string outputPath = Path.ChangeExtension(inputPath, "_flattened.pdf");
        using var outputFile = File.Create(outputPath);
        flattenedStream.CopyTo(outputFile);
    }
}