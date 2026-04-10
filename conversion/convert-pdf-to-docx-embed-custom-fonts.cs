// Global usings to satisfy the missing AsposePdfApi.GlobalUsings.g.cs file
global using System;
global using System.IO;
global using Aspose.Pdf;
global using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output DOCX and the custom font file
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string customFontPath = "MyCustomFont.ttf";

        // Validate file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Error: Custom font file not found – {customFontPath}");
            return;
        }

        // Register the custom font so that the conversion engine can embed it
        // In recent Aspose.PDF versions FontRepository.AddFont was removed.
        // Use FontRepository.Substitutions.Add with SimpleFontSubstitution instead.
        string fontFamilyName = Path.GetFileNameWithoutExtension(customFontPath);
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontFamilyName, customFontPath));

        // Load the PDF and convert it to DOCX with explicit save options
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX conversion options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format – DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Ensure Type3 fonts are converted to TrueType (text remains selectable)
                ConvertType3Fonts = true,

                // Re‑embed fonts in the resulting document
                ReSaveFonts = true,

                // Optional: cache glyphs for better performance
                CacheGlyphs = true
            };

            // Save the converted document
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}' (fonts embedded).");
    }
}
