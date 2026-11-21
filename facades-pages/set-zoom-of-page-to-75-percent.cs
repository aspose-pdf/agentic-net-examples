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

        // Use PdfPageEditor to set the initial view zoom for page 5
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new[] { 5 }; // 1‑based page number
            editor.Zoom = 0.75f;               // 75 % view
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 5 zoom set to 75 % and saved to '{outputPath}'.");
    }
}
