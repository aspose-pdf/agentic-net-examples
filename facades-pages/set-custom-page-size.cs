using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "customsize.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Define a non‑standard page size (width x height in points)
        // Example: 500 points wide by 700 points high
        editor.PageSize = new PageSize(500f, 700f);

        // Apply the size change to all pages
        editor.ApplyChanges();

        // Save the modified document
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Saved PDF with custom page size to '{outputPath}'.");
    }
}