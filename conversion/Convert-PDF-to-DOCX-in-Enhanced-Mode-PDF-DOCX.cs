using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, DocSaveOptions, etc.)

class PdfToDocxConverter
{
    static void Main()
    {
        // Paths to the source PDF and the destination DOCX file.
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX conversion in Enhanced Flow mode.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output format as DOCX.
                Format = DocSaveOptions.DocFormat.DocX,

                // EnhancedFlow provides full recognition, including tables, for maximum editability.
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Optional settings that often improve results; adjust as needed.
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as DOCX using the configured options.
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}