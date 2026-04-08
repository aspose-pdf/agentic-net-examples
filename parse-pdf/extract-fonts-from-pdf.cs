using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedFonts";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPdf))
            {
                // Retrieve all fonts referenced in the document
                Font[] fonts = doc.FontUtilities.GetAllFonts();

                foreach (Font font in fonts)
                {
                    // Determine a safe file name for the font
                    string fontName = font.DecodedFontName ?? font.BaseFont ?? "font";
                    foreach (char c in Path.GetInvalidFileNameChars())
                        fontName = fontName.Replace(c, '_');

                    string outPath = Path.Combine(outputDir, $"{fontName}.ttf");

                    // Export the font to a TTF file
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Saved font: {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}