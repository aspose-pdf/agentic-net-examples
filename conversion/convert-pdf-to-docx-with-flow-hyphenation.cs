using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Text;           // For TextEditOptions if needed (not used here)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output DOCX file path
        const string docxPath = "output.docx";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure DOCX conversion options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Use the Flow recognition mode for better text flow and hyphenation handling
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional: fine‑tune paragraph detection (helps hyphenation in some cases)
                    RelativeHorizontalProximity = 2.5f,
                    RecognizeBullets = true,

                    // Ensure the output format is DOCX
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as DOCX using the configured options
                pdfDocument.Save(docxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX at '{docxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}