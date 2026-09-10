using System;
using System.IO;
using Aspose.Pdf;                     // Core API (Document, DocSaveOptions, etc.)

class PdfToDocxConverter
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

        // Load the PDF, convert, and save as DOCX
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure conversion options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Preserve the original layout (standard/textbox mode)
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Output format – DOCX
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOCX using the specified options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}