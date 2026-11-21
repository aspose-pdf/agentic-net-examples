using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        // Paths to custom TrueType/OpenType font files that should be embedded
        string[] customFontFiles = { "fonts/CustomFont-Regular.ttf", "fonts/CustomFont-Bold.ttf" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Register custom fonts so the converter can locate and embed them
        foreach (string fontPath in customFontFiles)
        {
            if (File.Exists(fontPath))
            {
                // Derive the font name from the file name (without extension).
                // Adjust this if the PDF uses a different internal font name.
                string fontName = Path.GetFileNameWithoutExtension(fontPath);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontName, fontPath));
            }
            else
            {
                Console.Error.WriteLine($"Warning: font file not found: {fontPath}");
            }
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Directly save to PPTX. Custom fonts registered above will be embedded automatically.
                pdfDoc.Save(outputPptx, SaveFormat.Pptx);
            }

            Console.WriteLine($"Conversion successful. PPTX saved to: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
