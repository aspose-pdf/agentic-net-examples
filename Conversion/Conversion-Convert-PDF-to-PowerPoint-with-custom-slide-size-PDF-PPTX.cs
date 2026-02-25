using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is available, though not required for this conversion

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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Configure PPTX save options
                Aspose.Pdf.PptxSaveOptions pptxOptions = new Aspose.Pdf.PptxSaveOptions
                {
                    // ImageResolution (DPI) influences the size of the generated slides.
                    // Adjust as needed for custom slide dimensions.
                    ImageResolution = 150,

                    // Set to false to keep editable shapes; true would render each slide as a bitmap.
                    SlidesAsImages = false
                };

                // Save the PDF as a PPTX file using the configured options
                pdfDoc.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}