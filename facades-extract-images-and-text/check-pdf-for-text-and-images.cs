using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

public static class PdfAnalysis
{
    /// <summary>
    /// Returns true if the specified PDF contains at least one text fragment and at least one image.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    public static bool ContainsTextAndImages(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor.
            extractor.BindPdf(pdfPath);

            // ---------- Text extraction ----------
            // Extract the text from the whole document.
            extractor.ExtractText();

            // GetText requires a destination stream. Read the stream back into a string.
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    extractedText = reader.ReadToEnd();
                }
            }
            bool hasText = !string.IsNullOrWhiteSpace(extractedText);

            // ---------- Image extraction ----------
            // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // The default behavior extracts all images, so we simply call ExtractImage().
            extractor.ExtractImage();

            // HasNextImage returns true if at least one image is available after extraction.
            bool hasImages = extractor.HasNextImage();

            // Return true only if both text and images are present.
            return hasText && hasImages;
        }
    }
}

// A minimal entry point is required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // The Main method is intentionally lightweight; it exists solely to satisfy the compiler.
        // Example usage (can be removed in production):
        // if (args.Length > 0 && File.Exists(args[0]))
        // {
        //     bool result = PdfAnalysis.ContainsTextAndImages(args[0]);
        //     Console.WriteLine($"Contains text and images: {result}");
        // }
    }
}
