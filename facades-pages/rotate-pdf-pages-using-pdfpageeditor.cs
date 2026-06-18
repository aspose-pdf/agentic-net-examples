using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IFacade and is disposable, so use a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF file for editing
            editor.BindPdf(inputPath);

            // Example edit: rotate all pages by 90 degrees (allowed values: 0, 90, 180, 270)
            editor.Rotation = 90;

            // Apply the pending changes to the document
            editor.ApplyChanges();

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}