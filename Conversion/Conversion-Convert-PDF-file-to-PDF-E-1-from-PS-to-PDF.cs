using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PostScript file and output PDF file paths (generic names for cross‑platform demo)
        const string inputPsPath = "input.ps";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PS file exists
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Error: PostScript file not found – {inputPsPath}");
            return;
        }

        try
        {
            // Load the PS file using the correct PsLoadOptions class (not a nested type)
            var loadOptions = new PsLoadOptions();

            // Document constructor that accepts a Stream and LoadOptions
            using (var psStream = File.OpenRead(inputPsPath))
            using (var pdfDocument = new Document(psStream, loadOptions))
            {
                // Save the document as a regular PDF.
                // The SaveFormat.PdfE1 enum value is available only in newer Aspose.Pdf versions.
                // If your version does not contain it, fall back to SaveFormat.Pdf or upgrade the library.
                pdfDocument.Save(outputPdfPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"Conversion successful. PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}