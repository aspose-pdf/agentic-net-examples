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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Assign a custom progress handler to monitor conversion status
            pptxOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(
                (Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo info) =>
                {
                    // Simple console output showing progress percentage
                    Console.WriteLine($"Conversion progress: {info.Value}%");
                });

            // Save the document as PPTX using the configured options
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}