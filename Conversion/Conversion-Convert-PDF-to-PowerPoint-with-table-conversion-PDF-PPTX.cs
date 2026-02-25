using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize PPTX save options.
                // SlidesAsImages = false (default) keeps tables as editable PowerPoint tables.
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = false
                };

                // Save the PDF as a PPTX file using the specified options.
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}