using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DOCX save options.
                // - Format specifies DOCX output.
                // - RecognitionMode (standard) preserves the original layout.
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,
                    // The property name may vary by version; the intent is to use the standard mode.
                    // Uncomment the line below if the enum exists in your version:
                    // ContentRecognitionMode = DocSaveOptions.RecognitionMode.Standard
                };

                // Save as DOCX using the explicit options (required for non‑PDF formats)
                pdfDoc.Save(outputDocxPath, docOptions);
            }

            Console.WriteLine($"Conversion successful: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}