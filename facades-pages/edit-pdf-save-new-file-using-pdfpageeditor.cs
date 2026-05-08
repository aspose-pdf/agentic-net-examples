using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "original.pdf";   // original file (backup)
        const string outputPath = "edited.pdf";     // edited copy

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit the PDF using PdfPageEditor (e.g., change zoom)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);   // load original PDF
            editor.Zoom = 0.8f;          // example modification
            editor.Save(outputPath);     // save edited version to a new file
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'. Original file remains unchanged.");
    }
}