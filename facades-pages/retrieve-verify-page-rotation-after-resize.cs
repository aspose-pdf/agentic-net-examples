using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve rotation of the first page (pages are 1‑based)
            int originalRotation = editor.GetPageRotation(1);
            Console.WriteLine($"Original rotation (page 1): {originalRotation} degrees");

            // Change the page size – example uses A4 dimensions (595 x 842 points)
            // Aspose.Pdf.PageSize (in Aspose.Pdf namespace) provides the required constructor.
            editor.PageSize = new PageSize(595, 842);

            // Apply the size change and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        // Re‑open the resized PDF to verify that rotation is unchanged
        using (PdfPageEditor verifier = new PdfPageEditor())
        {
            verifier.BindPdf(outputPath);
            int newRotation = verifier.GetPageRotation(1);
            Console.WriteLine($"Rotation after resize (page 1): {newRotation} degrees");
        }
    }
}
