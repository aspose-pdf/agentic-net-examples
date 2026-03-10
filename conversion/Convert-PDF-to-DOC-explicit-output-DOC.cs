using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output DOC file path
        const string outputPath = "output.doc";

        // Verify that the source file exists
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
                // Configure DOC save options explicitly (required for non‑PDF output)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the target format as classic .doc
                    Format = DocSaveOptions.DocFormat.Doc,
                    // Use the Flow recognition mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Adjust horizontal proximity for word grouping
                    RelativeHorizontalProximity = 2.5f,
                    // Enable bullet recognition during conversion
                    RecognizeBullets = true
                };

                // Save the PDF as a DOC file using the configured options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during conversion
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}