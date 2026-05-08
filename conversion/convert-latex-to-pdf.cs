using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source LaTeX file and the desired PDF output.
        const string inputTexPath  = "input.tex";
        const string outputPdfPath = "output.pdf";

        // Verify that the LaTeX source file exists.
        if (!File.Exists(inputTexPath))
        {
            Console.Error.WriteLine($"Error: LaTeX file not found at '{inputTexPath}'.");
            return;
        }

        // Configure TeX loading options.
        // RasterizeFormulas = true ensures that complex equations are rendered correctly.
        // ShowTerminalOutput can be set to true for debugging the LaTeX compilation process.
        Aspose.Pdf.TeXLoadOptions texLoadOptions = new Aspose.Pdf.TeXLoadOptions
        {
            RasterizeFormulas = true,
            ShowTerminalOutput = false
        };

        // Load the LaTeX file and convert it to a PDF document.
        // The Document constructor takes the file path and the TeXLoadOptions instance.
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputTexPath, texLoadOptions))
        {
            // Save the generated PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Successfully converted LaTeX to PDF: '{outputPdfPath}'.");
    }
}