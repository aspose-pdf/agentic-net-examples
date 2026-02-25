using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler for conversion events
    static void ShowProgress(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        switch (info.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                Console.WriteLine($"{DateTime.Now}: Total progress {info.Value}%");
                break;
            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"{DateTime.Now}: Source page {info.Value} of {info.MaxValue} analysed.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                Console.WriteLine($"{DateTime.Now}: Result page {info.Value} of {info.MaxValue} created.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                Console.WriteLine($"{DateTime.Now}: Result page {info.Value} of {info.MaxValue} saved.");
                break;
        }
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load PDF, set custom metadata, and convert to PPTX
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Custom metadata (standard PDF Info dictionary)
            pdfDoc.Info.Title  = "Converted Presentation";
            pdfDoc.Info.Author = "Aspose.Pdf Sample";
            pdfDoc.Info.Subject = "PDF to PPTX conversion with metadata";

            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Example: render each slide as an image
                SlidesAsImages = false
            };

            // Attach progress handler
            pptxOptions.CustomProgressHandler =
                new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save as PPTX
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptx}'.");
    }
}