using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the two PDFs that have the same content but different compression settings
        const string pdfPath1 = "doc_compressed1.pdf";
        const string pdfPath2 = "doc_compressed2.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Extract textual content from each PDF
        string text1 = ExtractText(pdfPath1);
        string text2 = ExtractText(pdfPath2);

        // Compare the extracted texts; compression should not affect the result
        bool textsAreIdentical = string.Equals(text1, text2, StringComparison.Ordinal);

        Console.WriteLine(textsAreIdentical
            ? "Texts are identical; compression differences do not affect textual content."
            : "Texts differ; compression may have altered content or extraction differs.");

        // Optional diagnostics: show lengths of extracted text
        Console.WriteLine($"Length of PDF1 text: {text1.Length}");
        Console.WriteLine($"Length of PDF2 text: {text2.Length}");
    }

    // Helper method that extracts pure text from a PDF using TextAbsorber
    static string ExtractText(string pdfPath)
    {
        // Document is wrapped in a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            TextAbsorber absorber = new TextAbsorber();

            // Use pure text formatting mode to ignore layout and formatting differences
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages (pages are 1‑based internally)
            doc.Pages.Accept(absorber);

            // Return the concatenated text from the entire document
            return absorber.Text;
        }
    }
}