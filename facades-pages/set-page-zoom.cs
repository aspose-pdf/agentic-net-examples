using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            // Edit only page 3 (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };
            // Set zoom to 150% (1.5f corresponds to 150%)
            editor.Zoom = 1.5f;
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 zoom set to 150% and saved to '{outputPath}'.");
    }
}
