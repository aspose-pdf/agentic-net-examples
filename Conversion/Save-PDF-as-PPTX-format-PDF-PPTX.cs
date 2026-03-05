using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: using ensures disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create PPTX save options (explicit options required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX using the options
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}