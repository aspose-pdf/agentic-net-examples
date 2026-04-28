using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create PPTX save options and attach a custom progress handler
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Assign a lambda that matches the ConversionProgressEventHandler delegate.
            // The concrete type of the parameter (ProgressEventHandlerInfo) is inferred by the compiler,
            // so we avoid referencing the type name directly and eliminate the CS0246 error.
            pptxOptions.CustomProgressHandler = info =>
            {
                Console.WriteLine($"Event: {info.EventType}, Value: {info.Value}/{info.MaxValue}");
            };

            // Save the document as PPTX using the configured options
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptxPath}'.");
    }
}
