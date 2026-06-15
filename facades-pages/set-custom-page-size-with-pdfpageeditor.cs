using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "custom_sized.pdf";

        // Custom page dimensions in points (1 inch = 72 points)
        // Example: 8.5 x 11 inches => 612 x 792 points
        double customWidth  = 500; // replace with desired width
        double customHeight = 700; // replace with desired height

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that allows page manipulation.
        // It implements IDisposable via SaveableFacade, so we wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Set the desired page size for the output document.
            // PageSize constructor takes width and height in points.
            editor.PageSize = new PageSize((float)customWidth, (float)customHeight);

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}