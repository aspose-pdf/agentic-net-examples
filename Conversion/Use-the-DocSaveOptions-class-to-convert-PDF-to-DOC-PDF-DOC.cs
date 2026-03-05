using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, DocSaveOptions)

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
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, configure DOC save options, and save as DOC
        using (Document pdfDocument = new Document(inputPdfPath))   // Document is disposed automatically
        {
            // Initialize DocSaveOptions for Word DOC output
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the desired Word format (DOC)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Optional: improve bullet detection
                RecognizeBullets = true
            };

            // Save the PDF as a DOC file using the configured options
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
    }
}