using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
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

            // Configure DOCX conversion options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the enhanced flow mode (supports table recognition)
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Example of an additional custom option
                RecognizeBullets = true
            };

            // Save the PDF as a DOCX file using the configured options
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion completed successfully. DOCX saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
