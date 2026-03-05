using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, DocSaveOptions, etc.)

class PdfToDocConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output DOC file path
        const string outputDocPath = "output.doc";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the desired Word format (DOC)
                Format = DocSaveOptions.DocFormat.Doc,

                // Choose a recognition mode; Flow provides editable text
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Optional: enable bullet recognition for better list handling
                RecognizeBullets = true
            };

            // Save the PDF as a DOC file using the explicit save options
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocPath}'");
    }
}