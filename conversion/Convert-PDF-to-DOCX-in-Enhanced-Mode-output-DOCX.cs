using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, DocSaveOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";

        // Desired output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options for Enhanced Flow mode (best editability)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify that the target format is DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the EnhancedFlow recognition mode (supports tables and maximizes editability)
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Optional: fine‑tune conversion settings as needed
                RecognizeBullets = true,               // detect bullet lists
                RelativeHorizontalProximity = 2.5f,    // adjust word grouping sensitivity
                ConvertType3Fonts = true               // ensure Type3 fonts are converted to TrueType
            };

            // Save the PDF as a DOCX file using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}