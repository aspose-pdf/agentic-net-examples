using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and DocSaveOptions are in this namespace
using Aspose.Pdf.Text;          // Required for any text‑related types (not used here but safe)

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Desired output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists
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
                // Configure DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Preserve original layout as much as possible and recognize tables
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Output format: DOCX (Office Open XML)
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Convert Type3 fonts to TrueType to keep text selectable
                    ConvertType3Fonts = true,
                    // Optional: improve table detection by enabling bullet recognition
                    RecognizeBullets = true
                };

                // Save the PDF as DOCX using the explicit save options
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed successfully: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}