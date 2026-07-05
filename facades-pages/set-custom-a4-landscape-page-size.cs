using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_a4_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to modify page size.
        // The class implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Create a PageSize instance.
            // A4 size in points: 595 (height) x 842 (width) for portrait.
            // For landscape we swap them: width = 842, height = 595.
            // PageSize has no parameterless ctor, so instantiate with dummy values
            // and then set the Width and Height properties as required.
            PageSize customSize = new PageSize(0, 0);
            customSize.Width  = 842; // width in points (landscape)
            customSize.Height = 595; // height in points (landscape)

            // Assign the custom size to the editor.
            editor.PageSize = customSize;

            // Apply the changes to all pages.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Custom A4 landscape PDF saved to '{outputPath}'.");
    }
}