using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, DocSaveOptions)
using Aspose.Pdf.Facades;      // Included as requested (not directly used here)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Wrap the Document in a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOC save options (must be passed explicitly)
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Output format – DOC (not DOCX)
                    Format = DocSaveOptions.DocFormat.Doc,

                    // Use flow‑based text recognition for better layout
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional enhancements
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as a Word DOC file using the options
                pdfDocument.Save(outputDocPath, docOptions);
            }

            Console.WriteLine($"Conversion succeeded: '{outputDocPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}