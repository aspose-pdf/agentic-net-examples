using System;
using System.IO;
using Aspose.Pdf;   // Document, DocSaveOptions, etc.

class PdfToDocxConverter
{
    static void Main()
    {
        // Paths for the source PDF and the target DOCX file.
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF, configure conversion options, and save as DOCX.
        // The Document is wrapped in a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // DocSaveOptions controls how the PDF is converted to a Word document.
            // - Format = DocX produces a .docx file.
            // - Mode = Textbox creates editable text boxes (fast mode, preserves layout).
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX,
                Mode   = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the converted document using the specified options.
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}