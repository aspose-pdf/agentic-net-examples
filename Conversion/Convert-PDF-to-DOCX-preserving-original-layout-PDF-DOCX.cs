using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure save options for DOCX conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Preserve original layout as much as possible
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Ensure Type3 fonts are converted to TrueType for better text extraction
                ConvertType3Fonts = true,
                // Optional: recognize bullet lists during conversion
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
    }
}