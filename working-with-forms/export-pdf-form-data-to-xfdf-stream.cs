using System;
using System.IO;
using Aspose.Pdf;

/// <summary>
/// Provides functionality to export the XFDF (XML) representation of a PDF's form data
/// directly to a supplied <see cref="Stream"/>. This method is suitable for scenarios
/// where the caller wants to stream the data to an HTTP response without depending on
/// ASP.NET Core types (e.g., <c>Microsoft.AspNetCore.Http.HttpResponse</c>), which are
/// unavailable in a plain console or class‑library project.
/// </summary>
public class PdfFormExportHandler
{
    /// <summary>
    /// Exports the form annotations of the specified PDF file to the given output stream
    /// in XFDF (XML) format.
    /// </summary>
    /// <param name="outputStream">
    /// The destination stream. In a web scenario this would be the response body stream.
    /// The caller is responsible for disposing the stream if required.
    /// </param>
    /// <param name="pdfFilePath">Full path to the source PDF file.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="outputStream"/> is null.</exception>
    /// <exception cref="ArgumentException">If <paramref name="pdfFilePath"/> is null, empty or whitespace.</exception>
    /// <exception cref="FileNotFoundException">If the PDF file does not exist.</exception>
    public void ExportFormDataToStream(Stream outputStream, string pdfFilePath)
    {
        if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
        if (string.IsNullOrWhiteSpace(pdfFilePath))
            throw new ArgumentException("PDF file path is required.", nameof(pdfFilePath));
        if (!File.Exists(pdfFilePath))
            throw new FileNotFoundException("PDF file not found.", pdfFilePath);

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfFilePath))
        {
            // ExportAnnotationsToXfdf writes the XFDF XML directly into the provided stream.
            pdfDocument.ExportAnnotationsToXfdf(outputStream);
        }

        // Ensure all buffered data is flushed to the underlying destination.
        outputStream.Flush();
    }
}

// ---------------------------------------------------------------------------
// Example usage in a console application (or any non‑ASP.NET context).
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // This entry point satisfies the compiler requirement for an executable.
        // It demonstrates a simple usage of the PdfFormExportHandler.
        // In real scenarios the handler can be called from ASP.NET Core controllers
        // by passing HttpResponse.Body as the output stream.
        var handler = new PdfFormExportHandler();
        string pdfPath = "sample.pdf"; // adjust path as needed
        string outputPath = "formdata.xfdf";

        // Guard against missing sample file to avoid runtime exception during a dry run.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
        handler.ExportFormDataToStream(fileStream, pdfPath);
        Console.WriteLine($"XFDF export completed. Output saved to {outputPath}");
    }
}
