using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and DocSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options (non‑PDF format requires explicit options)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the desired output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Choose a recognition mode; Flow gives editable text
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition for better list handling
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}