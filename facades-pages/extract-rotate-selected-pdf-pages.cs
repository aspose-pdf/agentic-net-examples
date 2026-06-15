using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, pages to keep, and rotation angle (must be 0, 90, 180, or 270)
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_selected.pdf";
        int[] pagesToExtract = new int[] { 2, 4, 5 }; // example: keep pages 2,4,5
        int rotationAngle = 90; // rotate clockwise 90 degrees

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Temporary file to hold the extracted pages
        string tempExtractPath = Path.GetTempFileName();

        try
        {
            // ---------- Extract selected pages ----------
            // PdfFileEditor does not implement IDisposable; no using block needed
            PdfFileEditor extractor = new PdfFileEditor();
            bool extractSuccess = extractor.Extract(inputPath, pagesToExtract, tempExtractPath);
            if (!extractSuccess)
            {
                Console.Error.WriteLine("Failed to extract selected pages.");
                return;
            }

            // ---------- Rotate the extracted pages ----------
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(tempExtractPath);               // load the temporary PDF
            editor.Rotation = rotationAngle;               // apply rotation to all pages
            editor.ApplyChanges();                         // commit changes
            editor.Save(outputPath);                       // save final PDF

            Console.WriteLine($"Created PDF with selected pages rotated: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file
            try
            {
                if (File.Exists(tempExtractPath))
                    File.Delete(tempExtractPath);
            }
            catch { /* ignore cleanup errors */ }
        }
    }
}