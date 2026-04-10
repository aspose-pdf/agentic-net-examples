using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BrochureRotator
{
    static void Main()
    {
        const string inputPdf  = "brochure_input.pdf";
        const string outputPdf = "brochure_landscape.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfPageEditor (Facade) to rotate pages and set page size.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPdf);

            // Rotate all pages 90 degrees clockwise to landscape.
            editor.Rotation = 90; // Valid values: 0, 90, 180, 270

            // Ensure the output page size matches a standard brochure size (A4).
            // Landscape A4 will automatically be interpreted as width > height.
            editor.PageSize = PageSize.A4;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF. This uses the provided Save method.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Brochure pages rotated and saved to '{outputPdf}'.");
    }
}