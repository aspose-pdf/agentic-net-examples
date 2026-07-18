using System;
using System.IO;
using Aspose.Pdf;               // Core API for Document and save options
using Aspose.Pdf;               // PptxSaveOptions is in the same namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string dataDir   = "YOUR_DATA_DIRECTORY";
        string pdfPath   = Path.Combine(dataDir, "input.pdf");
        string pptxPath  = Path.Combine(dataDir, "output.pptx");

        // Verify source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: create & load)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize default PPTX save options
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save as PPTX (lifecycle: save)
            pdfDocument.Save(pptxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
    }
}