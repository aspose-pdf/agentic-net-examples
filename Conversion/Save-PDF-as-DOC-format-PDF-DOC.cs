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
            using (Document pdfDocument = new Document(inputPath))
            {
                // Configure DOC save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the output format as .doc (binary Word format)
                    Format = DocSaveOptions.DocFormat.Doc,
                    // Use Flow recognition mode for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional, improves conversion quality)
                    RecognizeBullets = true
                };

                // Save the PDF as a DOC file using the specified options
                pdfDocument.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully saved as DOC: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}