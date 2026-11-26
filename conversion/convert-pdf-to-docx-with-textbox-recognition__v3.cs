using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Set up DOCX conversion options as required
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use textbox recognition mode
                    Mode = DocSaveOptions.RecognitionMode.Textbox,
                    // Enable bullet detection
                    RecognizeBullets = true,
                    // Adjust horizontal proximity for layout analysis (float literal)
                    RelativeHorizontalProximity = 0.8f
                };

                // Save the PDF as a DOCX file using the configured options
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
