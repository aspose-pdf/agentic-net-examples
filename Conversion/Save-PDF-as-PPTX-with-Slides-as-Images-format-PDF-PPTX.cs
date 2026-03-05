using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the PPTX will be saved
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file path
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output PPTX file path
        string pptxPath = Path.Combine(dataDir, "output.pptx");

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create PPTX save options and configure to render each slide as an image
            PptxSaveOptions saveOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the PDF as PPTX using the configured options
            pdfDoc.Save(pptxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with slides as images: {pptxPath}");
    }
}