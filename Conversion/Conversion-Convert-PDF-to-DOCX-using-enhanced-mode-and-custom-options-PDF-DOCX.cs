using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Wrap the Document in a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX conversion options:
                // - EnhancedFlow mode provides full layout analysis and table recognition
                // - RecognizeBullets enables bullet detection
                // - RelativeHorizontalProximity adjusts line grouping sensitivity
                // - ConvertType3Fonts converts Type3 fonts to TrueType for editable text
                // - Format specifies the DOCX output format
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format                     = DocSaveOptions.DocFormat.DocX,
                    Mode                       = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    RecognizeBullets           = true,
                    RelativeHorizontalProximity = 2.5f,
                    ConvertType3Fonts          = true
                };

                // Save the PDF as DOCX using the custom options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}