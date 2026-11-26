using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions, including PptxSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "minutes.pdf";
        const string outputPath = "minutes.pptx";

        // Verify source file exists
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
                // Use the default PPTX save options; passing the options ensures non‑PDF output
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX
                pdfDoc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}