using System;
using System.IO;
using Aspose.Pdf;   // Aspose.Pdf namespace contains Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the target DOCX file.
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify that the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX conversion.
            // - Format specifies the output as DOCX.
            // - Mode set to Flow (automatic content detection) to let the engine decide the best layout.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX,
                Mode   = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the PDF as a DOCX file using the specified options.
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}