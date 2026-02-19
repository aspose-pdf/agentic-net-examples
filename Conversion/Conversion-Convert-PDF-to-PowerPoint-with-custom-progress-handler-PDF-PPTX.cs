using System;
using System.IO;
using Aspose.Pdf; // PptxSaveOptions resides in this namespace

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and output PPTX file paths.
        string inputPdfPath = "input.pdf";
        string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Configure PPTX save options.
            // NOTE: In the current Aspose.Pdf SDK the PptxSaveOptions class does NOT expose a
            //       ConversionProgress event/property, therefore we do not subscribe to it.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the PDF as a PPTX file using the configured options.
            pdfDocument.Save(outputPptxPath, pptxOptions);

            Console.WriteLine($"Conversion completed successfully. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}