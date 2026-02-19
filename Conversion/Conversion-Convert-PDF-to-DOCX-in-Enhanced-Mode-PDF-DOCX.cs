using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output DOCX
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure DOCX save options with EnhancedFlow recognition mode
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the EnhancedFlow mode to recognize tables and improve editability
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Explicitly set the output format to DOCX (optional, DOCX is default)
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOCX
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
