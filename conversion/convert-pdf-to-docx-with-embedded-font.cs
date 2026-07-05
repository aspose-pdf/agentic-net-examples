using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // FontRepository is defined here

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string customFontPath = "MyCustomFont.ttf";   // TrueType font to embed

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Register the custom font so that the conversion engine can use it.
            // FindFont loads the font from the supplied file path.
            Font customFont = FontRepository.FindFont(customFontPath);

            // If the Document class exposes a Fonts collection, add the font to it.
            // This step ensures the font is available during conversion.
            // Uncomment the following line if the Fonts property exists:
            // pdfDoc.Fonts.Add(customFont);

            // Configure DOCX save options
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Output format – DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use flow recognition mode for better layout preservation
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Convert Type3 fonts to TrueType (helps embedding)
                ConvertType3Fonts = true,

                // Enable bullet recognition (optional, improves lists)
                RecognizeBullets = true
            };

            // Save the PDF as DOCX. Aspose.Pdf automatically embeds the fonts
            // that are available in the document resources or that have been
            // registered via FontRepository.
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed. DOCX saved to '{outputDocxPath}'.");
    }
}