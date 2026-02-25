using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize PPTX save options (subclass of SaveOptions in Aspose.Pdf namespace)
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save the document as PPTX using explicit save options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }
}