using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "corrupted.pdf";

        // Verify the file exists before attempting to bind it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Attempt to bind the PDF. This will throw if the file is corrupted.
                extractor.BindPdf(inputPath);

                // If binding succeeds, you can proceed with extraction logic here.
                Console.WriteLine("PDF bound successfully.");
            }
        }
        // Aspose.Pdf throws PdfException for PDF‑related errors (including bind failures).
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PdfException: {ex.Message}");
        }
        // Catch any other unexpected exceptions.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}