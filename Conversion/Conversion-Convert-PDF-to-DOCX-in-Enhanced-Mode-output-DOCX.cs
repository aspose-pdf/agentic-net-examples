using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output DOCX path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPath);

            // Configure DOCX save options for Enhanced Flow mode.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the EnhancedFlow recognition mode (supports tables).
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Specify that the output format should be DOCX.
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOCX using the configured options.
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}