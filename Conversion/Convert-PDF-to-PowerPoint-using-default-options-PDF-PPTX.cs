using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination PPTX file.
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block to ensure proper disposal.
            using (Document pdfDocument = new Document(inputPath))
            {
                // Initialize the save options for PPTX conversion.
                PptxSaveOptions saveOptions = new PptxSaveOptions();

                // Save the document as a PowerPoint file using the specified options.
                pdfDocument.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during the conversion process.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}