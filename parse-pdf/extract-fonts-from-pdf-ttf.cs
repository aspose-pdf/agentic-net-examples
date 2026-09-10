using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "ExtractedFonts";     // folder to store .ttf files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Get all fonts used in the document
                // IDocumentFontUtilities is exposed via the FontUtilities property
                var fonts = doc.FontUtilities.GetAllFonts();

                foreach (Font font in fonts)
                {
                    // Build a safe file name for the font
                    string safeFontName = MakeSafeFileName(font.FontName);
                    string ttfPath = Path.Combine(outputFolder, $"{safeFontName}.ttf");

                    // Save the font to a TTF file
                    using (FileStream fs = new FileStream(ttfPath, FileMode.Create, FileAccess.Write))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Exported font: {font.FontName} -> {ttfPath}");
                }
            }

            Console.WriteLine("Font extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to replace invalid filename characters
    private static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}