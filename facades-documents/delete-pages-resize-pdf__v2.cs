using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    /// <summary>
    /// Deletes the specified pages from a PDF, resizes the remaining pages, and returns the result as a MemoryStream.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="pagesToDelete">Array of 1‑based page numbers to delete.</param>
    /// <param name="newWidth">New width for page contents (default units, 1 unit = 1/72 inch).</param>
    /// <param name="newHeight">New height for page contents (default units, 1 unit = 1/72 inch).</param>
    /// <returns>A MemoryStream containing the processed PDF.</returns>
    public static MemoryStream DeleteAndResize(string pdfPath, int[] pagesToDelete, double newWidth, double newHeight)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("Input PDF not found.", pdfPath);

        // Open the source PDF for reading.
        using (FileStream inputStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        // Temporary stream to hold the PDF after page deletion.
        using (MemoryStream afterDeleteStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages.
            editor.Delete(inputStream, pagesToDelete, afterDeleteStream);

            // Prepare the stream for the next operation.
            afterDeleteStream.Position = 0;

            // Stream that will receive the resized PDF.
            MemoryStream resultStream = new MemoryStream();

            // Resize contents of all remaining pages (null pages array means all pages).
            editor.ResizeContents(afterDeleteStream, resultStream, null, newWidth, newHeight);

            // Reset position so the caller can read from the beginning.
            resultStream.Position = 0;
            return resultStream;
        }
    }
}

// Minimal entry point required for a console application.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (commented out – keep the method library‑only if not needed).
        // string sourcePath = "input.pdf";
        // int[] pagesToDelete = { 1, 3 };
        // double newWidth = 595;   // A4 width in points (72 DPI)
        // double newHeight = 842;  // A4 height in points
        // using (MemoryStream processed = PdfProcessor.DeleteAndResize(sourcePath, pagesToDelete, newWidth, newHeight))
        // {
        //     using (FileStream outFile = new FileStream("output.pdf", FileMode.Create, FileAccess.Write))
        //     {
        //         processed.CopyTo(outFile);
        //     }
        // }
    }
}