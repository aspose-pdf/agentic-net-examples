using System;
using System.IO;
using Aspose.Pdf;               // Core API and all SaveOptions subclasses are here
using Aspose.Pdf.Text;          // For any text-related types if needed (not used here)

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPdfPath}");
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
                    // Preserve original layout as much as possible while recognizing tables
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Convert Type3 fonts to TrueType to keep text selectable
                    ConvertType3Fonts = true,
                    // Optional: improve table detection by enabling bullet recognition (does not affect layout)
                    RecognizeBullets = true
                };

                // Save the document as DOCX using the explicit save options
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}