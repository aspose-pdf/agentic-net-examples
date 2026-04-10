using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the PPTX will be saved.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfFile = Path.Combine(dataDir, "input.pdf");

        // Output PPTX file.
        string pptxFile = Path.Combine(dataDir, "output.pptx");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfFile}");
            return;
        }

        // Load the PDF document, convert and save as PPTX using default options.
        using (Document pdfDocument = new Document(pdfFile))
        {
            // Default conversion options for PPTX.
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save the document as PPTX.
            pdfDocument.Save(pptxFile, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxFile}");
    }
}