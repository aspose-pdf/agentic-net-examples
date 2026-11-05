using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Paths to custom TrueType/OpenType font files that should be embedded
        string[] customFontFiles = { "fonts/CustomFont-Regular.ttf", "fonts/CustomFont-Bold.ttf" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Register custom fonts via FontRepository.Substitutions (AddFont was removed in newer versions)
        foreach (string fontFile in customFontFiles)
        {
            if (File.Exists(fontFile))
            {
                // Use the file name (without extension) as the logical font name
                string fontName = Path.GetFileNameWithoutExtension(fontFile);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontName, fontFile));
            }
            else
            {
                Console.Error.WriteLine($"Warning: Font file not found: {fontFile}");
            }
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DOCX save options – only the Format property is required
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as DOCX; fonts are embedded automatically when possible
                pdfDoc.Save(outputDocxPath, docxOptions);
            }

            Console.WriteLine($"Conversion successful. DOCX saved to: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}