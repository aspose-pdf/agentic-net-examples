using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Text;          // Required for text-related options (if needed)

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output DOCX file path
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
                    // Specify the desired output format (DOCX)
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Choose a recognition mode; Flow provides editable text
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional: improve bullet detection and spacing
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as a DOCX file using the explicit save options
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