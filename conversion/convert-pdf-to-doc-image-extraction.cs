using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document and DocSaveOptions

class PdfToDocConverter
{
    static void Main()
    {
        // Paths to the source PDF and the destination DOC file
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOC conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOC (as opposed to DOCX)
                Format = DocSaveOptions.DocFormat.Doc,

                // Custom recognition mode – Textbox mode is fast and preserves layout,
                // which effectively extracts images without performing full text flow analysis.
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the PDF as a DOC file using the specified options
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
    }
}