using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block to guarantee disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure PPTX save options to render each slide as a raster image
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the document as PPTX using the specified options
            pdfDocument.Save(outputPptx, pptxOptions);
        }

        // Ensure the Aspose.Pdf assembly is not locked by the process before the next build.
        // The using statement above disposes the Document and releases all internal resources.
        // No additional code is required; just make sure the application has exited before rebuilding.

        Console.WriteLine($"Conversion completed: {outputPptx}");
    }
}
