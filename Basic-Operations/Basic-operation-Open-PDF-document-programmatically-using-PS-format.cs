using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PS file and where the PDF will be saved.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the PostScript (PS) file to be opened.
        string psFilePath = Path.Combine(dataDir, "sample.ps");

        // Path for the resulting PDF file.
        string pdfOutputPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the PS file exists before attempting to load it.
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Error: PS file not found at '{psFilePath}'.");
            return;
        }

        // Create load options specific for PS files.
        PsLoadOptions psLoadOptions = new PsLoadOptions();
        // Example option: convert non‑TrueType fonts to TrueType to reduce size.
        // psLoadOptions.ConvertFontsToTTF = true;

        // Open the PS file as a PDF document using the load options.
        using (Document pdfDocument = new Document(psFilePath, psLoadOptions))
        {
            // Save the opened document as a PDF file.
            // This follows the provided document-save rule.
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"Successfully converted PS to PDF. Output saved at: {pdfOutputPath}");
    }
}