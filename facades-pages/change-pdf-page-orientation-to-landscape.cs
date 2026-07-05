using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "landscape_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to modify page size/orientation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPath);

            // Create an A4 landscape PageSize (swap width/height of the default A4)
            PageSize landscapeA4 = new PageSize(PageSize.A4.Height, PageSize.A4.Width);
            landscapeA4.IsLandscape = true; // optional, clarifies orientation

            // Set the desired output page size
            editor.PageSize = landscapeA4;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}