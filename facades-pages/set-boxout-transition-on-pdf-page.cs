using System;
using System.IO;
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

        // Bind the PDF and apply a BoxOut transition (type = 1) with a 3‑second duration to page 2.
        // The PdfPageEditor uses integer values for TransitionType. In Aspose.Pdf the mapping is:
        //   0 = BoxIn, 1 = BoxOut, 2 = BlindHorizontal, 3 = BlindVertical, ...
        // Therefore we set TransitionType = 1 for BoxOut.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 2 }; // 1‑based page number to which the settings apply
            editor.TransitionType = 1;            // BoxOut transition
            editor.TransitionDuration = 3;        // duration in seconds
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
