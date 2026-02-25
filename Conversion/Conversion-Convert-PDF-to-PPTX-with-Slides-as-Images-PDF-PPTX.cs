using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and convert it to PPTX where each slide is an image of the page.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure PPTX save options to render each PDF page as an image.
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the document as PPTX using the specified options.
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with slides as images: {outputPptxPath}");
    }
}