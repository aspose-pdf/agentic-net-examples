using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfContentChecker
{
    /// <summary>
    /// Returns true if the specified PDF file contains at least one piece of text and at least one image.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file to be examined.</param>
    /// <returns>True when both text and images are present; otherwise false.</returns>
    public static bool ContainsTextAndImages(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // ---------- Check for text ----------
            // Extract all text from the document.
            extractor.ExtractText();

            // Capture the extracted text into a memory stream.
            bool hasText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                // If the stream contains any bytes, the PDF had text.
                hasText = textStream.Length > 0;
            }

            // ---------- Check for images ----------
            // Extract images from the document.
            extractor.ExtractImage();

            // HasNextImage returns true if at least one image is available.
            bool hasImage = extractor.HasNextImage();

            // Return true only when both conditions are satisfied.
            return hasText && hasImage;
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal entry point required for a console‑application build.
// This does not affect the library logic; it simply satisfies the compiler.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: AsposePdfApi <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            bool result = PdfContentChecker.ContainsTextAndImages(pdfPath);
            Console.WriteLine($"PDF contains both text and images: {result}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}