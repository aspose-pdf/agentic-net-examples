using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_a4_landscape.pdf";

        // Create a minimal source PDF so the sandbox has a file to open.
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPath);
        }

        // Bind the source PDF to the facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Define custom A4 landscape size using the PageSize constructor (float width, float height).
            var a4Landscape = new PageSize(842f, 595f); // width = 842 points (297 mm), height = 595 points (210 mm)

            // Apply the custom page size to all pages
            editor.PageSize = a4Landscape;
            editor.ApplyChanges();

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with A4 landscape size to '{outputPath}'.");
    }
}
