using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade for PDF page editing
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Pages to resize (2 and 4) – Aspose.Pdf uses 1‑based indexing
        int[] pagesToResize = new int[] { 2, 4 };

        // Create resize parameters with custom left/right margins (e.g., 20 units each)
        // Top and bottom margins are set to 0 (auto‑calculated)
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.Margins(20, 20, 0, 0);

        // Perform the resize operation; the method returns true on success
        bool result = fileEditor.ResizeContents(inputPath, outputPath, pagesToResize, resizeParams);

        if (result)
            Console.WriteLine($"Resize completed. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Resize operation failed.");
    }
}