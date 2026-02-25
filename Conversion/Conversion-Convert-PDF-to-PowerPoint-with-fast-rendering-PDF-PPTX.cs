using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string inputPdf  = "input.pdf";
        const string outputPpt = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Enable multi‑threaded processing for faster rendering
            // (IsMultiThreading is an instance field, not a static member)
            pptxOptions.IsMultiThreading = true;

            // Optional: render each slide as an image (faster, but non‑editable)
            // pptxOptions.SlidesAsImages = true;

            // Save the PDF as a PPTX presentation
            pdfDoc.Save(outputPpt, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPpt}");
    }
}