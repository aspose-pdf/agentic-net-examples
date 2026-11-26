using System;
using System.IO;
using Aspose.Pdf;   // LaTeXSaveOptions resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.tex";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create default LaTeX save options
            LaTeXSaveOptions latexOptions = new LaTeXSaveOptions();

            // Save the document as LaTeX; explicit options are required for non‑PDF formats
            doc.Save(outputPath, latexOptions);
        }

        Console.WriteLine($"PDF successfully converted to LaTeX: '{outputPath}'.");
    }
}