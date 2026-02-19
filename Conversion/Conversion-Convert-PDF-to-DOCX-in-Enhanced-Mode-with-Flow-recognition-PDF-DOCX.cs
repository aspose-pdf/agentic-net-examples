using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes and save options are in this namespace

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output DOCX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

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

            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use Enhanced Flow mode (recognizes tables and provides high editability)
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Specify the desired output format (DOCX)
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
