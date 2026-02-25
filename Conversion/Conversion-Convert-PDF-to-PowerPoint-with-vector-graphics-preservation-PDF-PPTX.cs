using System;
using System.IO;
using Aspose.Pdf; // Contains Document and PptxSaveOptions

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pptx";

        // Verify the source PDF exists
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
                // Initialize PPTX save options (vector graphics are preserved by default)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX, explicitly passing the save options
                pdfDoc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}