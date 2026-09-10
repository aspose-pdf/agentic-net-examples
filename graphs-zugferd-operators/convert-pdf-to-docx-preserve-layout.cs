using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace contains Document and DocSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";
        // Desired output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX (Office Open XML)
                Format = DocSaveOptions.DocFormat.DocX,
                // Use EnhancedFlow mode to recognize tables and preserve layout
                Mode   = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Convert Type3 fonts to TrueType to keep text selectable
                ConvertType3Fonts = true
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}