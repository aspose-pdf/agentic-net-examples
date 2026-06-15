using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that edits page layout.
        // It implements IDisposable, so use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Set horizontal alignment for all pages to left.
            // The enum is Aspose.Pdf.HorizontalAlignment.
            editor.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with left‑justified alignment: {outputPath}");
    }
}