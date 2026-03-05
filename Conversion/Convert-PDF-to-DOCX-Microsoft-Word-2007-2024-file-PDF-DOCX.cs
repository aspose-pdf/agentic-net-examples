using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and all SaveOptions subclasses are here

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX save options.
                // DocSaveOptions resides in the Aspose.Pdf namespace (no separate sub‑namespace).
                // The Format property selects .docx output, and Mode controls the recognition strategy.
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,   // .docx output
                    Mode   = DocSaveOptions.RecognitionMode.Flow // full text flow recognition (editable)
                };

                // Save the PDF as DOCX using the explicit SaveOptions.
                // This follows the rule: non‑PDF saves must provide a SaveOptions instance.
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}