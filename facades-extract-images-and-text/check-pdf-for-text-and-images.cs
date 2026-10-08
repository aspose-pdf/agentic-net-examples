using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnalysis
{
    /// <summary>
    /// Returns true if the specified PDF contains at least one text fragment and at least one image.
    /// Uses Aspose.Pdf.Facades.PdfExtractor for the checks.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>True when both text and images are present; otherwise false.</returns>
    public static bool ContainsTextAndImages(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        // PdfExtractor implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // ---------- Text check ----------
            // Extract text from the document.
            extractor.ExtractText();

            // Retrieve the extracted text via a MemoryStream.
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    extractedText = reader.ReadToEnd();
                }
            }

            // Determine if any non‑whitespace text was found.
            bool hasText = !string.IsNullOrWhiteSpace(extractedText);

            // ---------- Image check ----------
            // The PdfExtractor extracts all images by default; no need to set a non‑existent ExtractImageMode.
            extractor.ExtractImage();

            // Count extracted images using the iterator methods.
            int imageCount = 0;
            while (extractor.HasNextImage())
            {
                // Advance the iterator – we do not need to persist the image.
                using (MemoryStream dummy = new MemoryStream())
                {
                    extractor.GetNextImage(dummy);
                }
                imageCount++;
            }

            bool hasImage = imageCount > 0;

            // Return true only when both conditions are satisfied.
            return hasText && hasImage;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a library project this method can be removed.
    public static void Main(string[] args)
    {
        // No operation – the class is intended to be used programmatically.
    }
}