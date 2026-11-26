using System;
using System.IO;
using Aspose.Pdf; // DocSaveOptions resides directly in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
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
                // Configure DOCX save options
                var docOptions = new DocSaveOptions
                {
                    // Set the recognition mode (Flow)
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet detection
                    RecognizeBullets = true,
                    // Adjust proximity threshold (float literal required)
                    RelativeHorizontalProximity = 0.3f,
                    // Specify the output format explicitly (optional, default is DocX when using DocSaveOptions)
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as a DOCX file using the configured options
                pdfDoc.Save(outputPath, docOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
