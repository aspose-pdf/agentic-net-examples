using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, DocSaveOptions, etc.

class PdfToDocConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Desired output DOC file path
        const string outputDocPath = "output.doc";

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
                // Configure DOC save options with Flow recognition mode
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the target format (DOC, not DOCX)
                    Format = DocSaveOptions.DocFormat.Doc,

                    // Set the recognition mode to Flow for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as a DOC file using the configured options
                pdfDocument.Save(outputDocPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed successfully: '{outputDocPath}'");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}