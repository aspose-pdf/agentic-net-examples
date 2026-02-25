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

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX conversion options (Enhanced Mode)
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Output format – DOCX
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Use Flow recognition mode for better layout preservation
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional enhancements
                    RecognizeBullets = true,                 // Detect bullet lists
                    RelativeHorizontalProximity = 2.5f       // Tuning for paragraph detection
                };

                // Save the PDF as a DOCX file using the configured options
                pdfDocument.Save(outputDocxPath, docOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            // Catch any conversion‑related exceptions and report them
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}