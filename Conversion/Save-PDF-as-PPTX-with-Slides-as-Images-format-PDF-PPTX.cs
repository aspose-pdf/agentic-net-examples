using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output PPTX file path
        const string outputPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create PPTX save options (lifecycle: create)
            PptxSaveOptions saveOptions = new PptxSaveOptions
            {
                // Convert each PDF page to an image on a separate slide
                SlidesAsImages = true
            };

            // Save the document as PPTX using the specified options (lifecycle: save)
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with slides as images: '{outputPath}'.");
    }
}