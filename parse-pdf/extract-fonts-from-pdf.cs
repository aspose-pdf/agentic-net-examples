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

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle: using)
            using (Document doc = new Document(inputPdf))
            {
                // Retrieve all fonts used in the document via the FontUtilities property
                Font[] fonts = doc.FontUtilities.GetAllFonts();

                foreach (Font font in fonts)
                {
                    // Determine a safe file name for the font
                    string fontName = font.FontName ?? font.BaseFont ?? "font";
                    foreach (char c in Path.GetInvalidFileNameChars())
                        fontName = fontName.Replace(c, '_');

                    string ttfPath = Path.Combine(outputDir, $"{fontName}.ttf");

                    // Export the font to a TTF file (font.Save writes TTF format)
                    using (FileStream fs = new FileStream(ttfPath, FileMode.Create, FileAccess.Write))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Saved font: {ttfPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
