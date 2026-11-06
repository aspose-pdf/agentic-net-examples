using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Binds a PDF from a byte array, exports its annotations to XFDF,
    /// and returns the XFDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The PDF file content.</param>
    /// <returns>XFDF data as a byte array.</returns>
    public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            throw new ArgumentException("PDF byte array is null or empty.", nameof(pdfBytes));

        // Input stream containing the PDF.
        using (var pdfStream = new MemoryStream(pdfBytes))
        // Output stream that will hold the XFDF data.
        using (var xfdfStream = new MemoryStream())
        // PdfAnnotationEditor provides annotation handling in the Facades API.
        using (var annotationEditor = new PdfAnnotationEditor())
        {
            // Bind the PDF from the input stream.
            annotationEditor.BindPdf(pdfStream);

            // Export annotations to the XFDF stream.
            // Use the overload that accepts page range and a null annotation‑type filter.
            annotationEditor.ExportAnnotationsXfdf(
                xfdfStream,
                1,
                annotationEditor.Document.Pages.Count,
                (string[])null);

            // Return the XFDF bytes.
            return xfdfStream.ToArray();
        }
    }
}

// Dummy entry point – required when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank.
    }
}