using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, DocSaveOptions, etc.

class PdfToDocConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output DOC file path
        const string outputDocPath = "output.doc";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOC save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the desired Word format (DOC)
                    Format = DocSaveOptions.DocFormat.Doc,

                    // Use the Flow recognition mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional: improve bullet detection and text proximity handling
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as a DOC file using the configured options
                pdfDocument.Save(outputDocPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}