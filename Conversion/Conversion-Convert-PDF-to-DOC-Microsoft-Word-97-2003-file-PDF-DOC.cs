using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.doc";

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
                // Configure DOC save options (Word 97‑2003)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the target format
                    Format = DocSaveOptions.DocFormat.Doc,
                    // Use Flow recognition for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional settings to improve conversion quality
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOC using explicit save options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Conversion succeeded: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}