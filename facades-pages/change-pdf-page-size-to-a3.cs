using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_A3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and edit its pages using the Facades API.
        // PdfPageEditor implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Set the desired output page size to A3.
            // PageSize.A3 corresponds to 420 mm × 297 mm.
            editor.PageSize = PageSize.A3;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF to the specified output path.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF page size changed to A3 and saved as '{outputPath}'.");
    }
}