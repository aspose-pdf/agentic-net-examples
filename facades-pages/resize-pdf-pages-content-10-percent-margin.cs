using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
            PdfFileEditor fileEditor = new PdfFileEditor();

            // Resize all pages to 90% of their original width and height.
            // Passing null for the pages array means “all pages”.
            // Margins are calculated automatically (default parameters).
            bool result = fileEditor.ResizeContents(
                source:      inputPath,
                destination: outputPath,
                pages:       null,
                newWidth:    90,   // 90% of original width → 10% left/right margin
                newHeight:   90);  // 90% of original height → 10% top/bottom margin

            if (result)
                Console.WriteLine($"Pages resized successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Resize operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}