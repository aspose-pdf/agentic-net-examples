using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output DOCX file path
        const string outputDocxPath = "output.docx";

        // Path to a custom TrueType font that should be embedded in the DOCX
        const string customFontPath = "customfont.ttf";

        // Verify that the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        try
        {
            // Load the custom font and register it as a substitution for any missing fonts.
            // This ensures the font is available during conversion and will be embedded.
            using (FileStream fontStream = File.OpenRead(customFontPath))
            {
                // Open the font (TTF format)
                Font customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);

                // Example substitution: replace missing "Helvetica" with the custom font.
                // Adjust the source font name as needed for your PDF.
                FontRepository.Substitutions.Add(
                    new SimpleFontSubstitution("Helvetica", customFont.FontName));
            }

            // Load the source PDF document.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX save options.
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX as the target format.
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Enable re‑saving of fonts so they are embedded in the output DOCX.
                    ReSaveFonts = true,

                    // Optional: convert Type3 fonts to TrueType to improve text extraction.
                    ConvertType3Fonts = true,

                    // Use flow recognition mode for better layout preservation.
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as DOCX with the configured options.
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX with embedded fonts: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}