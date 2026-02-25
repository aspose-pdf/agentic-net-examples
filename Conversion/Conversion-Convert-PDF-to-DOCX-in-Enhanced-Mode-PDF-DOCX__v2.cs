using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options for Enhanced Flow mode (tables are recognized)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Use the EnhancedFlow recognition mode
                    Mode   = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Explicitly set the output format to DOCX
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}