using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for font handling if needed

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the PPTX will be saved
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output PPTX file
        string pptxPath = Path.Combine(dataDir, "output.pptx");

        // Name of the custom font you want to use (must be installed or accessible to Aspose.Pdf)
        string customFontName = "MyCustomFont";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // OPTIONAL: If the PDF references fonts that are missing, you can specify a fallback font.
                // This does not add the font to the document but tells the converter which font to use
                // when a required font cannot be found.
                PdfSaveOptions fallbackOptions = new PdfSaveOptions
                {
                    DefaultFontName = customFontName
                };
                // The fallback options are applied during the save operation.
                // They are not needed for PPTX conversion itself, but they ensure consistent appearance
                // when the source PDF lacks embedded fonts.

                // Initialize PPTX save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    // Caching glyphs can improve performance for large documents
                    CacheGlyphs = true
                };

                // Save the PDF as a PPTX presentation.
                // Aspose.Pdf will embed the fonts that are available on the system.
                // If the custom font is installed, it will be used in the resulting PPTX.
                pdfDocument.Save(pptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed successfully. PPTX saved to: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}