using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options for Enhanced Flow mode
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Enhanced flow mode provides better editability and table recognition
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Optional: improve conversion quality
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}