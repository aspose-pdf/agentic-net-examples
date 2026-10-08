using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfPageEditor resides in this namespace

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

        // Set the display duration of page 5 to 5 seconds using PdfPageEditor.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 5 }; // select page 5 (1‑based index)
            editor.DisplayDuration = 5;            // duration in seconds
            editor.ApplyChanges();                  // apply the changes before saving
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
