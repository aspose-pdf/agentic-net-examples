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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure save options for DOCX output
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX as the target format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow recognition mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional)
                    RecognizeBullets = true
                };

                // Save the PDF as a DOCX file using the explicit save options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}