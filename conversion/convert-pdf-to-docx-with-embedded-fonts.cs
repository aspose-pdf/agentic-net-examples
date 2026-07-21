using System;
using System.IO;
using Aspose.Pdf; // Document, DocSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output DOCX file path
        const string outputDocx = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Configure DOCX conversion options
                var saveOptions = new DocSaveOptions
                {
                    // Save as DOCX (correct enum value)
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Use the Flow recognition mode for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Convert Type3 fonts to TrueType so they appear as text, not images
                    ConvertType3Fonts = true,

                    // Re‑save fonts on each page to ensure they are embedded in the DOCX
                    ReSaveFonts = true,

                    // Optional: cache glyphs for better performance during conversion
                    CacheGlyphs = true
                };

                // Save the converted document as DOCX with the specified options
                pdfDocument.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"Conversion completed successfully. DOCX saved to '{outputDocx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
