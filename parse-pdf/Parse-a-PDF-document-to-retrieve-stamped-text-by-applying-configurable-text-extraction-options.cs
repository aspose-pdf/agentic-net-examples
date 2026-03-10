using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfStampTextExtractor
{
    /// <summary>
    /// Extracts all text (including text added via stamps) from a PDF document
    /// using the specified text formatting mode.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <param name="mode">
    /// Desired formatting mode for extraction:
    /// Pure, Raw, Flatten or MemorySaving.
    /// </param>
    /// <returns>The extracted text as a single string.</returns>
    public static string ExtractStampedText(string pdfPath, TextExtractionOptions.TextFormattingMode mode)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Configure extraction options with the requested formatting mode.
            TextExtractionOptions extractionOptions = new TextExtractionOptions(mode);

            // Create a TextAbsorber that uses the configured options.
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Apply the absorber to all pages of the document.
            doc.Pages.Accept(absorber);

            // The absorber now contains the extracted text, including any text
            // that was added via stamp annotations.
            return absorber.Text;
        }
    }
}

// Simple console entry point so the project builds as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // Expect the PDF path as the first argument; if not supplied, show usage.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: AsposePdfApi <pdfPath> [formattingMode]");
            Console.WriteLine("formattingMode options: Pure, Raw, Flatten, MemorySaving (default: Pure)");
            return;
        }

        string pdfPath = args[0];
        // Parse optional formatting mode; default to Pure if parsing fails.
        TextExtractionOptions.TextFormattingMode mode = TextExtractionOptions.TextFormattingMode.Pure;
        if (args.Length > 1 && Enum.TryParse(args[1], true, out TextExtractionOptions.TextFormattingMode parsedMode))
        {
            mode = parsedMode;
        }

        try
        {
            string stampedText = PdfStampTextExtractor.ExtractStampedText(pdfPath, mode);
            Console.WriteLine("--- Extracted Text ---");
            Console.WriteLine(stampedText);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting text: {ex.Message}");
        }
    }
}
