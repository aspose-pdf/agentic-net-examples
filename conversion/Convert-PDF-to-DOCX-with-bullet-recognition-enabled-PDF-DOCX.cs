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

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet recognition during conversion
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}