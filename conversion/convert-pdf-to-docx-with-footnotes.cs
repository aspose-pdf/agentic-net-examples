using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, DocSaveOptions, etc.)

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdfPath  = "input.pdf";

        // Output DOCX file (extension determines format)
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, convert, and save using a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure conversion options:
            // - Use EnhancedFlow mode for the most accurate content recognition (tables, footnotes, etc.)
            // - Set additional options if needed (e.g., ConvertType3Fonts = true to handle Type3 fonts)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Enhanced recognition for better footnote handling
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Optional: improve handling of Type3 fonts (often used for footnote symbols)
                ConvertType3Fonts = true
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}