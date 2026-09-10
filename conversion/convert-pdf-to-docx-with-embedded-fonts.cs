using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use flow recognition for better layout
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable font re‑saving so that fonts are embedded in the resulting DOCX
                ReSaveFonts = true,

                // Convert Type3 fonts to TrueType (helps preserve text instead of images)
                ConvertType3Fonts = true,

                // Optional: improve bullet detection
                RecognizeBullets = true
            };

            // Save the PDF as DOCX with the specified options
            pdfDocument.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX with embedded fonts: {outputDocxPath}");
    }
}