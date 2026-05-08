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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve all fonts used in the document
            Font[] fonts = doc.FontUtilities.GetAllFonts();

            foreach (Font font in fonts)
            {
                // Export only embedded fonts (they contain actual font data)
                if (font.IsEmbedded)
                {
                    // Create a safe file name based on the font's name
                    string safeName = MakeSafeFileName(font.FontName ?? "font");
                    string outPath = Path.Combine(outputDir, $"{safeName}.ttf");

                    // Save the font to a .ttf file via a stream
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Saved font: {outPath}");
                }
            }
        }
    }

    // Helper method to replace invalid filename characters
    static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}