using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the directory of the executing assembly as the base data directory.
        // This avoids the placeholder "YOUR_DATA_DIRECTORY" that caused the runtime error.
        string dataDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input PDF file path.
        string pdfFile = Path.Combine(dataDir, "input.pdf");

        // Verify that the source PDF exists before attempting conversion.
        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfFile}'.");
            return;
        }

        // Output PPTX file path.
        string pptxFile = Path.Combine(dataDir, "output.pptx");

        // Load the PDF document and convert it to PPTX using default options.
        using (Document pdfDocument = new Document(pdfFile))
        {
            // Initialize default PPTX save options.
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save the document as PPTX.
            pdfDocument.Save(pptxFile, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxFile}");
    }
}
