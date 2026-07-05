using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfContentChecker
{
    /// <summary>
    /// Returns true if the specified PDF file contains both text and at least one image.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>True when both text and images are present; otherwise false.</returns>
    public static bool ContainsTextAndImages(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // PdfExtractor implements IDisposable, so use a using block for deterministic disposal.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // ---------- Check for text ----------
            // Extract all text from the document.
            extractor.ExtractText();

            bool hasText;
            // Retrieve the extracted text into a memory stream and examine its content.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    hasText = !string.IsNullOrWhiteSpace(extractedText);
                }
            }

            // ---------- Check for images ----------
            // Extract images from the document.
            extractor.ExtractImage();

            // HasNextImage returns true if at least one image is available after extraction.
            bool hasImage = extractor.HasNextImage();

            // Return true only when both conditions are satisfied.
            return hasText && hasImage;
        }
    }
}

// Simple console entry point so the project compiles as an executable.
public class Program
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
