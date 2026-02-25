using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace – contains Document, PptxSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure PPTX save options. No special settings are required for hyperlink
                // preservation – the default conversion keeps them.
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Convert and save the PDF as a PowerPoint presentation.
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