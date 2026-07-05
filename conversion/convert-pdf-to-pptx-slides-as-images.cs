using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PPTX save options and enable raster image rendering for each slide
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the document as PPTX using the configured options
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPptx}");
    }
}