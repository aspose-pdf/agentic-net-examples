using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PPTX file path
        const string outputPptx = "output.pptx";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an instance of PptxSaveOptions (default constructor)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the PDF as PPTX using the save options (rule: explicit SaveOptions for non‑PDF formats)
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}