using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfJavaScriptStripper resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF with form fields
        const string outputPath = "output_no_js.pdf";   // destination PDF without JavaScript

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfJavaScriptStripper removes all JavaScript (including form‑field scripts) from a PDF.
        // The Strip method works with file paths directly; no explicit Document object is needed.
        PdfJavaScriptStripper jsStripper = new PdfJavaScriptStripper();
        jsStripper.Strip(inputPath, outputPath);

        Console.WriteLine($"All JavaScript actions removed. Saved to '{outputPath}'.");
    }
}