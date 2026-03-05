using System;
using System.IO;
using Aspose.Pdf;

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
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize save options for PPTX conversion
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Attach a progress handler to receive conversion percentage updates
                // The delegate signature matches the CustomProgressHandler expected by Aspose.Pdf
                pptxOptions.CustomProgressHandler = progress =>
                {
                    Console.WriteLine($"Conversion progress: {progress}%");
                };

                // Save the document as PPTX using the configured options
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}