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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOC save options with Flow recognition mode
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.Doc,               // Output as .doc
                    Mode   = DocSaveOptions.RecognitionMode.Flow,       // Full flow recognition
                    RecognizeBullets = true,                            // Optional: improve bullet detection
                    RelativeHorizontalProximity = 2.5f                  // Optional: tweak text grouping
                };

                // Save the document as DOC using the save options (required for non‑PDF formats)
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}