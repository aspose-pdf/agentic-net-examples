using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Desired output DOCX file path
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Preserve original layout and tables
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Ensure Type3 fonts are converted to TrueType for proper text extraction
                ConvertType3Fonts = true,
                // Set output format to DOCX
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}