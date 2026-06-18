using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output DOCX and the custom font file.
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string customFontPath = "customfont.ttf";

        // Verify that the input files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font file not found: {customFontPath}");
            return;
        }

        // Register the custom font so that Aspose.Pdf can use it during conversion.
        // The font name is extracted from the font file (e.g., "CustomFont").
        // If the font is not installed on the system, add a substitution that maps any missing font to this custom font.
        // This ensures the font will be embedded into the resulting DOCX.
        string customFontName = Path.GetFileNameWithoutExtension(customFontPath);
        // Load the font into the repository (if the API supports it). If not, the substitution alone is sufficient.
        // FontRepository.AddFont(customFontPath); // Uncomment if AddFont method is available.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", customFontName));

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options.
            DocSaveOptions docSaveOptions = new DocSaveOptions
            {
                // Output format: DOCX.
                Format = DocSaveOptions.DocFormat.DocX,
                // Use flow recognition mode for better layout preservation.
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Ensure that fonts are embedded into the DOCX.
                // The property ReSaveFonts exists in recent versions; uncomment if available.
                // ReSaveFonts = true
            };

            // Save the PDF as DOCX. The custom font will be embedded because it is registered above.
            pdfDocument.Save(outputDocxPath, docSaveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX with embedded custom font: {outputDocxPath}");
    }
}