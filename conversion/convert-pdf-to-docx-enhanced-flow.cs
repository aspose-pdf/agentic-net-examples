using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, DocSaveOptions)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";

        // Output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF, configure conversion options, and save as DOCX
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the enhanced flow mode which supports complex tables and graphics
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Optional: preserve the original layout as much as possible
                // (you can adjust other properties here if needed)
                // Example: enable bullet recognition
                RecognizeBullets = true
            };

            // Save the document as DOCX using the specified options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}