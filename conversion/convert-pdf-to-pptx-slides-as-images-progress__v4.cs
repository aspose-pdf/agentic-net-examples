using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "contract.pdf";
        const string outputPath = "contract_full.pptx";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure PPTX save options
            var pptxOptions = new PptxSaveOptions
            {
                // Render each slide as an image to preserve appearance
                SlidesAsImages = true
            };

            // Attach a custom progress handler using the supported CustomProgressHandler property
            pptxOptions.CustomProgressHandler = info =>
            {
                // Display progress information
                Console.WriteLine($"Conversion progress: {info.Value}% (Event: {info.EventType})");
                // No need to return a value; returning true/false is not required for this delegate
            };

            // Save the PDF as PPTX using the explicit save options
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
