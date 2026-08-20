using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfContentChecker
{
    /// <summary>
    /// Returns true if the specified PDF file contains both text and images.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>True when at least one text fragment and one image are present.</returns>
    public static bool ContainsTextAndImages(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file.
            extractor.BindPdf(pdfPath);

            // ----- Check for text -----
            // Extract all text from the document.
            extractor.ExtractText();

            // Capture extracted text into a memory stream.
            bool hasText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                hasText = textStream.Length > 0;
            }

            // If there is no text, we can return false early.
            if (!hasText)
                return false;

            // ----- Check for images -----
            // Extract images from the document.
            extractor.ExtractImage();

            // HasNextImage indicates whether at least one image is available.
            bool hasImage = extractor.HasNextImage();

            // Return true only when both text and image are present.
            return hasImage;
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal console entry point – required for a project that compiles to an
// executable.  If the project is intended to be a class library, change the
// output type instead of adding this class.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PdfContentChecker <pdfPath>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            bool containsBoth = PdfContentChecker.ContainsTextAndImages(pdfPath);
            Console.WriteLine($"PDF contains both text and images: {containsBoth}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}