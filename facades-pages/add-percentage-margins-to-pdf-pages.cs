using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        // Pages to which the margin will be applied (1‑based indexing)
        int[] pages = new int[] { 1, 2, 3, 4 }; // adjust as needed

        // 15 % margin on each side
        double leftMargin   = 15;
        double rightMargin  = 15;
        double topMargin    = 15;
        double bottomMargin = 15;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Apply percentage margins using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.AddMarginsPct(
            inputPath,
            outputPath,
            pages,
            leftMargin,
            rightMargin,
            topMargin,
            bottomMargin);

        if (result)
            Console.WriteLine($"Margins applied successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to apply margins.");
    }
}