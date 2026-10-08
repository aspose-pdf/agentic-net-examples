using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1️⃣ Align the content of page 5 to the centre and set its display duration to 4 seconds.
        // ------------------------------------------------------------
        // PdfPageEditor handles both alignment and display duration. We select page 5 via
        // the ProcessPages property, then apply the required settings.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 5 };               // 1‑based page number
            editor.HorizontalAlignment = HorizontalAlignment.Center;
            editor.VerticalAlignmentType = VerticalAlignment.Center;
            editor.DisplayDuration = 4; // seconds
            editor.ApplyChanges(); // ensure all changes are committed
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page alignment and display duration updated. Output saved to '{outputPath}'.");
    }
}
