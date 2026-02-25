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
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options for enhanced conversion.
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX as the target format.
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use EnhancedFlow mode for better structure and table recognition.
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Optional enhancements.
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOCX using the specified options.
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}