using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output PPTX path as arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToPptxConverter <input.pdf> <output.pptx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPath);

            // Configure PPTX save options.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            // Optional: enable full font embedding (uncomment if needed).
            // pptxOptions.FontEmbeddingMode = FontEmbeddingMode.EmbedAllFonts;

            // Save the document as PPTX with the specified options.
            pdfDocument.Save(outputPath, pptxOptions);

            Console.WriteLine($"Conversion succeeded. PPTX saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}