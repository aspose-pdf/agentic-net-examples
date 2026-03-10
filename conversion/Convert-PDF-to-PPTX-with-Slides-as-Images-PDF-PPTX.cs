using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pptx";

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
                // Configure PPTX save options
                PptxSaveOptions saveOptions = new PptxSaveOptions
                {
                    // Render each PDF page as an image on a separate slide
                    SlidesAsImages = true
                };

                // Save the document as PPTX using the specified options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}