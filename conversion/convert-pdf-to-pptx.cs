using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document and all SaveOptions)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pptx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF and convert to PPTX using default options
        using (Document pdfDoc = new Document(inputPath))
        {
            // PptxSaveOptions is the required options class for PPTX output.
            // Using the default constructor applies the library's default conversion settings.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }
}