using System;
using System.IO;
using Aspose.Pdf; // PptxSaveOptions resides in this namespace

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PPTX path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToPptxConverter <input.pdf> <output.pptx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputPptxPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found – '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // OPTIONAL: configure enhanced conversion mode.
            // The PptxSaveOptions class provides a RecognitionMode property.
            // Setting it to EnhancedFlow enables table‑aware conversion.
            // If the property or enum is unavailable in the referenced version,
            // the code will still compile and use the default conversion mode.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            // Uncomment the following line if the enum/value exists in your version:
            // pptxOptions.RecognitionMode = PptxSaveOptions.RecognitionMode.EnhancedFlow;

            // Save the document as PPTX.
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}