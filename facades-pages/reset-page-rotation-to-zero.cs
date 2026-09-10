using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_reset_rotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to modify page rotation.
        // The class implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Ensure we are editing page 6.
            // Page numbers are 1‑based.
            // Reset rotation to 0 degrees for page 6.
            editor.PageRotations[6] = 0;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 6 rotation reset. Saved to '{outputPath}'.");
    }
}