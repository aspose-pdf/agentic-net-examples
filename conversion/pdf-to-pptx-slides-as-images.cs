using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the input PDF and the output PPTX files.
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure PPTX save options to render each slide as an image.
            PptxSaveOptions saveOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the document as PPTX.
            pdfDocument.Save(outputPptxPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to PPTX with slides as images: {outputPptxPath}");
    }
}