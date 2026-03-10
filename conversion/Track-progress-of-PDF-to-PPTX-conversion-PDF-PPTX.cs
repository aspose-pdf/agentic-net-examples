using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF and will receive the PPTX output.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "PDF-to-PPTX.pdf");
        // Output PPTX file.
        string pptxPath = Path.Combine(dataDir, "PDF-to-PPTX.pptx");

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize PPTX save options.
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Attach a custom progress handler to receive conversion events.
            saveOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Optional: export each slide as an image instead of editable shapes.
            // saveOptions.SlidesAsImages = true;

            // Perform the conversion and save the result.
            pdfDocument.Save(pptxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: {pptxPath}");
    }

    // Progress handler invoked by Aspose.Pdf during conversion.
    static void ShowProgress(PptxSaveOptions.ProgressEventHandlerInfo info)
    {
        // Example output: "TotalProgress: 45/100"
        Console.WriteLine($"{info.EventType}: {info.Value}/{info.MaxValue}");
    }
}
