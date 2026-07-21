using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";

        // Directory where extracted TTF files will be saved
        const string outputDir = "ExtractedFonts";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Retrieve all fonts used in the document via the IDocumentFontUtilities interface
                Font[] fonts = doc.FontUtilities.GetAllFonts();

                foreach (Font font in fonts)
                {
                    // Use the decoded font name for a readable file name; fallback to FontName if empty
                    string fontName = string.IsNullOrWhiteSpace(font.DecodedFontName)
                                      ? font.FontName
                                      : font.DecodedFontName;

                    // Sanitize file name (remove invalid characters)
                    foreach (char c in Path.GetInvalidFileNameChars())
                        fontName = fontName.Replace(c, '_');

                    // Build full path for the .ttf file
                    string ttfPath = Path.Combine(outputDir, $"{fontName}.ttf");

                    // Save the font to the .ttf file using the Font.Save(Stream) method
                    using (FileStream fs = new FileStream(ttfPath, FileMode.Create, FileAccess.Write))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Extracted font: {fontName} -> {ttfPath}");
                }
            }

            Console.WriteLine("Font extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during font extraction: {ex.Message}");
        }
    }
}