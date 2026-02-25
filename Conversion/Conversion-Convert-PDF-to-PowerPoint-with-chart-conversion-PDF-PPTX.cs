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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize PPTX save options (required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PowerPoint; charts in the PDF are converted to PPTX chart objects
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPptx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}