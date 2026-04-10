using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main()
    {
        // Paths to the source PDF and the target DOCX file.
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Ensure the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX conversion.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output format as DOCX.
                Format = DocSaveOptions.DocFormat.DocX,
                // Set the recognition mode to Flow, which enables automatic content detection.
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the PDF as a DOCX file using the configured options.
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}